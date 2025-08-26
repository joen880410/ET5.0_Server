const HotfixOpcode = require("./Message/HotfixOpcode");
var protobuf = require("protobufjs");
const ws = require('ws');
const Apis = require("./src/Apis");
const webSocket = new ws.WebSocket('ws://172.20.10.6:8888');
module.exports = {
    Send: Send,
}
var responsedata;
var requestCallback = {};

(function init() {
    webSocket.on('message', data => {
        const Opcode = parseInt(data.subarray(0, 2).reverse().toString("hex"), 16);
        const responseMessage = HotfixOpcode.GetOpcodeName(Opcode);
        protobuf.load("HotfixMessage.proto", function (error, root) {
            response = root.lookupType(`ETHotfix.${responseMessage}`);
            responsedata = response.decode(data.subarray(2, data.length));
            requestCallback[responsedata.RpcId](responsedata);
            delete requestCallback[responsedata.RpcId];
        });
    });
})();

async function Send(requestMessage, info) {
    var rpcId = Apis.rpcId
    return new Promise((resolve, reject) => {
        protobuf.load("HotfixMessage.proto", function (error, root) {
            request = root.lookupType(`ETHotfix.${requestMessage}`);
            let errMsg = request.verify(info);
            if (errMsg)
                throw Error(errMsg);
            const data = request.create(info);
            const Opcode = HotfixOpcode.GetOpcode(requestMessage);
            const bytes = Buffer.from(Opcode.toString(16), "hex").reverse();
            let writer = new protobuf.Writer();
            writer.bytes(new Uint8Array(bytes));
            let buffer = request.encode(data, writer).finish();
            buffer = buffer.subarray(1, buffer.length);
            rpcId++;
            requestCallback[info.RpcId] = (res) => {
                resolve(res);
            };
            webSocket.send(buffer);
        });
    });
}


