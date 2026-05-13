# Roadmap de implementación - Lethal Shuffle v1
Este roadmap describe las fases de desarrollo para llegar a un prototipo jugable PvE, partiendo de la base de que ya tenemos un sistema de turnos muy básico, y un sistema de cartas y de mazo (a excepción de las cartas de entidad y de objeto).

## Fase 1: Tablero y Entidades
- [x] Implementar el tablero con 3 slots de personajes y 3 slots de objetos por cada lado.
- [x] Implementar las Cartas de Entidad (heredando de Card).
- [x] Implementar un sistema que reconozca cuando se ha jugado una carta (al soltarla).
- [x] Permitir invocar las cartas donde corresponda según su tipo:
    - [x] Si es una Carta de Entidad: debería soltarse en un slot de entidad.
    - [x] Si es una carta de Habilidad: podría soltarse en cualquier zona del tablero.
- [x] Mostrar al campeon del jugador al iniciar el juego (el campeón existe en el DeckTemplate)
- [x] Crear prefabs distintos para cada tipo de carta, para que se vean diferentes en la UI
- [x] Lograr que la preview de las cartas (al hacer hover) sea la adecuada según el tipo de carta (Ability, Entity...)

## Fase 2: Enemigos PvE
- [x] Introducir el concepto de Campeón como entidad fija (por ahora existirá un único campeón por mazo).
- [ ] Introducir el boss como entidad enemiga fija en el lado opuesto.
- [ ] En la escena de Test, crear un boss con dos adds
- [ ] Agregar funcionalidad a las fases de ataque del jugador y del enemigo

## Fase 3: Desarrollo y resolución del combate
- [ ] Definir un patrón básico de comportamiento/enemigo para el boss.
- [ ] Considerar oleadas como comportamiento de ciertos bosses si el combate lo requiere.
- [ ] Resolver daño de las entidades en orden sobre objetivos enemigos disponibles.
- [ ] Aplicar la regla del primer turno si corresponde.

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
