# Cartas
## Cartas de Campeón
Cada baraja debe contener un "Mazo de Cartas de Campeón", con 2 Cartas de Campeón.
Al iniciar una partida, se juega automáticamente una carta de Campeón, la cual genera una Entidad de Campeón.
Cuando un Campeón es derrotado, se juega la siguiente carta del Mazo de Cartas de Campeón.
Si un Campeón es derrotado, y su jugador no tiene más Cartas de Campeón disponibles, pierde la partida.

## Cartas de Juego
Cada baraja debe contener un "Mazo de Cartas de Juego", con un máximo de 40 cartas de Juego.
Estas son las cartas que se roban y juegan en el transcurso de la partida.
Pueden ser **Cartas de Entidad**, **Cartas de Habilidad** o **Cartas de Objeto**.

## Etiquetas (Tags)
Las Etiquetas (Tags) simbolizan una característica del Campeón (por ejemplo, "Martial" o "Leader").
Durante la partida, sólo podrán jugarse cartas con etiquetas que aparezcan en la carta del Campeón activo.
Sin embargo, las cartas ya en juego no se pierden, aún si el Campeón activo cambia por otro que no tenga las etiquetas necesarias.
Por ejemplo, una entidad seguirá en juego aún si el primer Campeón muere y el segundo ya no tiene sus etiquetas, pero si esta carta es devuelta a la mano, no podrá volver a jugarse.


# Gameplay
## **Entidad (Entity)**
Una entidad es cualquier criatura que exista sobre el tablero. Los jugadores pueden tener en su baraja Cartas de Entidad, las cuales al utilizarse invocan una Entidad sobre un espacio vacío.

> **Matiz importante**: Una carta de entidad NO es una Entidad, sino un método para invocar una.

### Tipos de Entidades
Existen 2 tipos de entidades:
- **Entidad de Campeón**
    - En el caso de los Jugadores, esta será la entidad que represente sus capacidades. Algunas cartas tienen requisitos específicos, y sólo podrán usarse si la entidad de campeón activa los cumple. Además, si los puntos de vida de una Entidad de Campeón llegan a 0, se deberá invocar al siguiente del Mazo de Campeones. Si el último Campeón es derrotado, ese jugador pierde la partida.
    - En el caso de los enemigos (en el modo PvE), el **Boss** sería el equivalente a una Entidad de Campeón. A efectos prácticos funciona del mismo modo, excepto por la parte de los requisitos, que por cómo están programados los enemigos no aplicaría esta limitación.
- **Entidad Invocada** [(WIP)]
	- En el caso de los jugadores, estas 

### Estadísticas de las Entidades
Las entidades siempre tienen puntos de ataque y puntos de vida, y algunas de ellas también tienen efectos especiales.
- **Puntos de Ataque**: Al final del turno, las Entidades golpearán en orden a los enemigos. Esta estadística determina cuánto daño sufrirá la Entidad objetivo.
- **Puntos de Vida**: Cada vez que una Entidad sufre daño, esto se ve reflejado en una reducción de sus Puntos de Vida. Si los Puntos de Vida de una Entidad llegan a 0, esta es destruida y eliminada del tablero.
- **Efectos**: Estos efectos se aplican igual que los de las Cartas de Objeto, ya que mientras la Entidad siga en pie sobre el tablero, su efecto seguirá aplicándose cuando corresponda.
	- Por ejemplo, *"Mientras esta carta esté en el tablero, su Campeón recupera 2 Puntos de Vida al inicio de cada turno"* sería un efecto que aplicaría en todos los turnos, hasta que la Entidad fuese destruida.



