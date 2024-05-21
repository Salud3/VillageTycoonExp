#VillageTycoon - Prototype

Juego experimental tipo tycoon de mejora de un poblado a traves de las mecanicas de un tycoon tradicional, mejora de estaciones de trabajo, ganancia de "oro" por la venta de recursos, mejora de una aldea hasta una ciudad con el "oro" obtenido

Scripts:
 * Clases utilizadas para el almacenamiento de datos.
  - WalletClass.
  - VillagerClass.
  - TutorialClass.

- Savesystem: Guarda y administra los datos por medio de Json para cada clase previamente mencionada.
- GameManager: Contiene las funciones de calculo y actualiza el almacenamiento de datos, asi como manda a llamar un Save All cada 250 segundos.
- VillagerManager: Maneja la logica interna de los aldeanos, asignandoles puestos de trabajo y definiendo un tipo de aldeano, ademas contiene la maquina de estados por medio de enums para los aldeanos y
  -  AIVillager : Incluye la toma de deciciones por medio de una maquina de estados definida en el VillagerManager, ademas de "trabajar" para poder vender su producto.
- IAManager: Maneja la logica interna de los clientes, su objetivo y su spawn, ademas contiene la maquina de estados por medio de enums para los clientes.
  -  IACostumer : controlador de movimiento y desicion de cada cliente conforma a la maquina de estados definido en el AIManager, asi como su velocidad y comportamientos dependiendo de la tienda en la que comprara.
- Canvas Controller: Maneja los menus desplegables y revisa la informacion almacenada por medio del save System como cuantas mejoras has comprado y cuales.
- DialogueManager: Maneja los diferentes dialogos que se mostraran por medio del tutorial.
- Dialogue: Por medio de corrutinas y Metodos, se hace una escritura caracter por caracter para dar como resultado un dialogo gradual.
