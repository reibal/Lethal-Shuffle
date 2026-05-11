# Roadmap de implementación - Lethal Shuffle v1
Este roadmap describe las fases de desarrollo para llegar a un prototipo jugable PvE, partiendo de la base de que ya tenemos un sistema de turnos muy básico, y un sistema de cartas y de mazo (a excepción de las cartas de entidad y de objeto).

## Fase 1: Tablero y Entidades
- [x] Implementar el tablero con 3 slots de personajes y 3 slots de objetos por cada lado.
- [x] Implementar las Cartas de Entidad (heredando de Card).
- [ ] Implementar un sistema que reconozca cuando se ha jugado una carta (al soltarla).
- [ ] Permitir invocar las cartas donde corresponda según su tipo:
    - [ ] Si es una Carta de Entidad: debería soltarse en un slot de entidad.
    - [ ] Si es una Carta de Objeto: debería soltarse en un slot de objeto.
    - [ ] Si es una carta de Habilidad: podría soltarse en cualquier zona del tablero.
- [ ] Asegurar que los objetos colocados debajo de un personaje puedan aplicarse a ese personaje cuando correspondan.
- [ ] Ignorar por ahora los campeones y las cartas de campeón.
- [ ] Implementar las Cartas de Objeto (heredando de Card).

## Fase 2: Enemigos PvE
- [ ] Introducir el boss como entidad enemiga fija en el lado opuesto.
- [ ] Agregar adds/enemigos invocables u objetos de boss según el diseño.
- [ ] Definir un patrón básico de comportamiento/enemigo para el boss.
- [ ] Considerar oleadas como comportamiento de ciertos bosses si el combate lo requiere.

## Fase 3: Resolución del combate
- [ ] Implementar la fase de ataque automático al acabar el turno del jugador.
- [ ] Resolver daño de las entidades en orden sobre objetivos enemigos disponibles.
- [ ] Aplicar la regla del primer turno si corresponde.
- [ ] Agregar la fase de ataque del enemigo en su turno.

## Fase 4: Economía y validaciones
- [ ] Añadir un sistema de maná y coste de cartas.
- [ ] Validar la jugabilidad de cartas en base a coste y tags del campeón activo cuando el sistema de campeones esté listo.
- [ ] Implementar los efectos de cartas de habilidad y cartas de entidad.

## Fase 5: Campeones
- [ ] Implementar el campeón activo del jugador como entidad especial en el tablero.
- [ ] Agregar recambio de campeón cuando uno muera.
- [ ] Más tarde, permitir dos campeones y el paso automático al siguiente.

## Fase 6: Fin de partida
- [ ] Definir condiciones de victoria y derrota.
- [ ] Mostrar pantalla placeholder de victoria o derrota.
- [ ] Validar la experiencia de juego completa: inicio de partida, turnos, combate, resultado.

## Notas
- El GDD conserva el diseño de mecánicas, mientras que este archivo se usa para planificar la implementación.
- El orden de fases ayuda a evitar dependencias tempranas: primero tablero y combate, luego recursos y campeones.
