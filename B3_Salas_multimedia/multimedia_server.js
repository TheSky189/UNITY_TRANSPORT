const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 3002 });

const rooms = {};  // { roomName: Set of clients }

wss.on('connection', function connection(ws) {
    console.log('Nuevo cliente conectado');
    ws.on('message', function incoming(data) {
    const messageStr = data.toString(); 
    console.log('Mensaje recibido: ', messageStr);
    const message = JSON.parse(messageStr);
        
        if (message.type === 'join') {
            const room = message.room;
            ws.room = room;
            rooms[room] = rooms[room] || new Set();
            rooms[room].add(ws);
            ws.send(`Te has unido a la sala: ${room}`);
        } else if (message.type === 'message') {
            const room = ws.room;
            if (room && rooms[room]) {
                rooms[room].forEach(client => {
                    if (client.readyState === WebSocket.OPEN) {
                        client.send(`${message.sender}: ${message.content}`);
                    }
                });
            }
        }
    });

    ws.on('close', () => {
        const room = ws.room;
        if (room && rooms[room]) {
            rooms[room].delete(ws);
        }
    });
});
