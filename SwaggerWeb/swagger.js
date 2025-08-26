const swaggerAutogen = require('swagger-autogen')()


const doc = {
    info: {
        version: "1.0.0",
        title: "Jacfit Apis",
        description: "My User Project Application API",
    },
    host: "localhost:8000",
    basePath: "/",
    schemes: ['http', 'https'],
    consumes: ['application/json'],
    produces: ['application/json'],
    securityDefinitions: {
        basicAuth: {
            type: "basic",
        },
        petstore_auth: {
            type: "oauth2",
            authorizationUrl: "https://petstore.swagger.io/oauth/authorize",
            flow: "implicit",
            scopes: {
                // read_pets: "read your pets",
                // write_pets: "modify pets in your account"
            }
        }
    },
    definitions: {

    }
}

const outputFile = './swagger.json'
const endpointsFiles = ['./src/Apis.js']

swaggerAutogen(outputFile, endpointsFiles, doc)
//.then(() => {require('./index')})           // Your project's root file
