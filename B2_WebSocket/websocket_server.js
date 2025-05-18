const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 3002 });

wss.on('connection', function connection(ws) {
    console.log('Cliente conectado');

    ws.on('message', function incoming(message) {
        console.log('Mensaje recibido: %s', message);
        ws.send(`Servidor recibió: ${message}`);
    });

    ws.send('¡Bienvenido al servidor WebSocket!');
});
