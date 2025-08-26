const express = require("express");
const app = express();
const call = require("./Client2Server.js");
const swaggerFile = require('./swagger.json')
const bodyParser = require("body-parser");
const swaggerUi = require("swagger-ui-express");
//app.use(cors())
app.use(bodyParser.json())
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerFile));

app.get("/user/:id", (req, res) => {
  var info = {
    RpcId: 0,
    Info:
    {
      Language: 41, Secret: "64BA18C5E54E7A722B1878C1459DC99B94A8F603F8961AF0B86B3673F03E4D88268FC8E8E6B6F790A6C186D7F19B2546", FirebaseDeviceToken: "", Type: 1, DeviceId: "", VerId: "", info:
      {
        Email: "", DeviceModel: ""
      }
    }
  };
  call.Send("C2R_Authentication", info).then((value) => {
    call.Send("C2G_LoginGate", { RpcId: 1, Key: value.Key }).then((Result) => {
      res.status(200).json({
        success: "true",
        Result: Result,
      });
    });
  });
});
app.listen(8000, () => {
  console.log("server listening on port 8000!");
});
require('./src/Apis')(app)
