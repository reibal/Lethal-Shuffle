# Lethal Shuffle v1
La primera versión del juego será algo parecido al **Slay The Spire**; será un juego PvE en el que el jugador utilizará una baraja propia (personalizable), mientras que los enemigos aparecerán en el tablero en el lado del oponente: habrá un **Boss** (que haría el papel de “Campeón”), y **adds** (que harían el papel de Entidades invocadas).

En algunos combates, es posible que no existan adds y únicamente haya un Boss (probablemente con estadísticas y patrones con una dificultad escalada para que el combate esté equilibrado, pese a ir contra un único enemigo).

De igual manera, algunas etapas funcionarán con un sistema de oleadas en el que, al vencer a todas las Entidades Invocadas, el Boss podrá invocar más.

En cualquier caso, la victoria en el modo PvE se logra cuando se derrota finalmente al Boss, independientemente de si han quedado adds por derrotar.

## Flujo (simplificado) de la partida
### Inicio de la partida
Al iniciar la partida, **cada jugador roba 5 cartas** (de momento sólo existirá un jugador, ya que en el modo PvE el enemigo tendrá su propio sistema sin cartas).
El tablero consistirá de dos mitades, una para cada jugador. En los extremos opuestos estarán las entidades de Campeón/Boss. Delante de las mismas, habrá dos espacios en cada mitad para colocar otras entidades.

### Turno del jugador
El jugador tomará el primer turno.

En cada inicio de turno ocurrirá lo siguiente:
- El jugador roba una carta de su Mazo de Cartas de Juego
- El jugador recupera 2 puntos de Maná

Después de esto, el jugador podrá utilizar las cartas que quiera; por ejemplo, podrá invocar Entidades, usar habilidades ofensivas o defensivas, etc.

#### Cierre del turno del jugador
Al finalizar el turno del jugador, sus entidades atacarán automáticamente al primer objetivo disponible, en orden. Si en uno de estos ataques una entidad es derrotada, los siguientes ataques irán contra la siguiente entidad disponible. Cuando no queden entidades enemigas en juego, los ataques irán directamente contra el Campeón enemigo. La única excepción es el primer turno del primer jugador, en el cual se anula el daño contra el Campeón enemigo. 

### Turno del enemigo (PvE)
En caso de PvP, el turno del enemigo sigue el mismo flujo que el turno del jugador.
Sin embargo, en el PvE el funcionamiento es ligeramente distinto: No existen cartas para los enemigos, y en su lugar tienen patrones (que pueden ser estrictos e ir en orden, o ser semi-aleatorios). Estos patrones pueden consistir en habilidades ofensivas, defensivas, estratégicas o de invocación de Entidades en espacios vacíos (esto último sería algo específico de ciertos bosses y no una mecánica general). 

Sin contar este método de invocaciones por patrón, la única manera de que un enemigo cree nuevas Entidades como mecánica general es, en los enfrentamientos con oleadas, cuando todas sus entidades son derrotadas, puede invocar la siguiente oleada. **IMPORTANTE**: esta invocación por oleadas SOLO puede ocurrir al iniciar el turno del enemigo (es decir, en caso de que el jugador derrote a todos los adds y siga teniendo ataques disponibles, podrá golpear al Boss directamente durante el resto de este turno).

Una vez se ejecutan todas las acciones en base al patrón del enemigo, vuelve a iniciar el turno del jugador.

### Cambio de Campeón y condiciones de Victoria
En cualquier momento, un Campeón puede ser derrotado. Si en ese momento queda otra carta de campeón disponible, este es invocado automáticamente.
Cuando el último campeón disponible de un jugador es derrotado, dicho jugador pierde la partida.
Lo mismo aplica con un Boss en el PvE: si el Boss muere, es una victoria para el jugador (aunque queden esbirros vivos).

## Tipos de Cartas
Cada baraja consta de dos partes: 
- Cartas de Campeón (únicamente 2 Cartas de Campeón)
- Cartas de Juego (máximo 40 Cartas de Juego)

Entre los distintos tipos de cartas de juego encontraremos:
- **Cartas de Campeón** (en el Mazo de Campeones)
	- Son las cartas que invocan una Entidad de Campeón, la cual representa al personaje activo mientras esté en juego como Entidad. Tendrán una estadística de Vida, y (opcionalmente) Efectos.
	- Cuando una Entidad de Campeón es derrotada, se juega la siguiente Carta de Campeón. Si el último Campeón de un jugador es derrotado (es decir, no quedan más cartas en el Mazo de Campeones), este pierde la partida.
- **Cartas de Juego** (en el Mazo de Juego)
	- Estas serán las cartas que se roben y se jueguen a lo largo de la partida.
	- Las cartas que vayan a jugarse dependerán de las **Tags** que tenga el Campeón activo, y si este requisito no se cumple, la carta no podrá ser jugada. Por ejemplo, si un jugador tiene en juego a un Campeón con las Tags "Martial" y "Leader", sólo podrá jugar cartas con Tag "Martial", "Leader", o "None".
	- Cada carta tendrá un **coste de Maná**, que deberá pagarse para poder utilizarla.

### Cartas de Juego
Existirán 3 tipos distintos de **Cartas de Juego**:
- **Cartas de Habilidad**: Son Cartas que tienen un **efecto inmediato** (por ejemplo: infligir daño, curar vida, o dar ciertos beneficios o desventajas). Sólo pueden usarse por campeones que tengan la Tag que requiere la carta.
- **Cartas de Entidad**: Son Cartas que permiten **invocar una entidad sobre el tablero**. Las entidades tienen estadísticas de Daño, Vida y (opcionalmente) Efectos. Sólo pueden usarse por campeones que tengan la Tag que requiere la carta.
- **Cartas de Objeto**: Son cartas que otorgan ciertos **efectos especiales de manera permanente**, mientras estén en el tablero. Estas cartas no tienen Tags asociadas, por lo que pueden ser usadas con cualquier Campeón.
	- Cada jugador sólo podrá tener 2 objetos al mismo tiempo, cada uno "vinculado" con un espacio de Entidad. Esto debe ser así porque algunos objetos tendrán efectos sobre la Entidad "vinculada". Los objetos cuyos efectos no guarden relación con la Entidad, simplemente ocuparán ese espacio y aplicarán su efecto en general.
	- Se está valorando que los objetos tengan una duración (en turnos) y después desaparezcan, de manera que requiera un factor estratégico adicional para decidir cuándo usarlos.
	- *Más adelante quizá se añadirán "Cartas de Objeto Maldito", que seguirán la misma lógica que las Cartas de Objeto, pero se utilizarán sobre los espacios de objeto del enemigo, y darán ventajas e inconvenientes (cada carta maldita debe dar al menos una ventaja) de manera forzosa al rival, además de bloquear ese espacio de objeto.
		- Esto, por razones obvias, es complicado a nivel de balanceo, por lo que por el momento no se implementará todavía.*

