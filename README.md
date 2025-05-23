
# Proyecto M09 - Unity Transport

- Para probar necesitas descargar unitypackage/zip y el video /.mp4:
```
https://drive.google.com/drive/folders/19f3wlbEfKcMpxgQUG8buQsDn7yBHD-tI?usp=sharing
```

## Descripcion

- Chat por protocolo **UDP**  
- Chat usando **WebSocket**  
- Carga de contenido web desde un servidor **Node.js**  
- Reproducción de **vídeo en streaming**

---

## Instrucciones de Uso

### 1️. Chat UDP

-  Ir a la carpeta `Servers/B1_Chat_UDP`  
- Run Unity.  
- Introducir mensaje en el InputField y enviarlo.

-  **Puerto por defecto:** `5005`  

 **Cerrar Comunicación UDP:**  
Cierra la aplicación Unity o la escena actual.

---

### 2️. Chat WebSocket

-  Ir a la carpeta `Servers/B2_WebSocket`  
-  Iniciar el servidor WebSocket:
```bash
node websocket_server.js
```
-  **Puerto por defecto:** `3001`

 **Cerrar Servidor WebSocket:**  
Pulsa `Ctrl + C` en la terminal.

---

### 3️. Servidor Web (Carga de Contenido Web)

-  Ir a `Servers/B01_NodeJs`  
-  Iniciar el servidor Web:
```bash
node server.js
```
-  **Puerto por defecto:** `3000`  

 **Cerrar Servidor Web:**  
Pulsa `Ctrl + C` en la terminal.

---

### 4️. Streaming de Vídeo

-  Ir a `Servers/B4_Streaming_proyecto_final`  
-  Iniciar el servidor de streaming:
```bash
node streaming_server.js
```
-  **Puerto por defecto:** `3003`  

 **Acceder al vídeo directamente desde el navegador o Unity:**  
```
http://localhost:3003/PROYECTO_V3.mp4
```

** El vídeo tienes que descargar porque es demasiado grande, el link esta abajo, se llama PROYECTO_V3.mp4:**  

```
https://drive.google.com/drive/folders/19f3wlbEfKcMpxgQUG8buQsDn7yBHD-tI?usp=sharing
```

 **Cerrar Servidor de Streaming:**  
Pulsa `Ctrl + C` en la terminal.

---

##  Configuración para Comunicación entre Dos Ordenadores (Unity)

  1. Asegúrate de que ambos dispositivos estén conectados a la misma red local.  
  2. Obtén la IP del ordenador que actuará como **servidor** ejecutando en la terminal (cmd):

```bash
ipconfig
```

Abre el script ChatManager.cs en Unity y modifica la IP según el protocolo que estés utilizando.

 Para UDP:
```bash
udpClient.Connect("192.168.X.X", 5000);  // Sustituye con la IP del servidor
```

 Para WebSocket:
```bash
websocket = new WebSocket("ws://192.168.X.X:3001");  // Sustituye con la IP del servidor
```
  Guarda los cambios y ejecuta la aplicación en ambos dispositivos.

  El servidor debe tener los servicios de Node.js en ejecución.
  El cliente debe usar la IP correcta configurada en el código.

  4. Guarda y ejecuta los scripts en ambas máquinas.

---

##  **Dependencias Externas Necesarias**

### Unity:

-  **NativeWebSocket (para WebSocket en Unity)**  
Instalación desde Unity Package Manager: Windows>Package Manager> Add package from git URL :
```
https://github.com/endel/NativeWebSocket.git
```
Más información: [NativeWebSocket en GitHub](https://github.com/endel/NativeWebSocket)

---

### Node.js:

-  Express (para el servidor de streaming y WebSocket)  
Instalación:
```bash
npm install express ws
```
