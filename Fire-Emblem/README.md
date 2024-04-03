# Fire Emblem Combat Simulator

Este proyecto es un simulador de combate basado en el universo de Fire Emblem, un popular videojuego de táctica y estrategia. El simulador permite a los jugadores crear equipos de personajes, cada uno con sus propias estadísticas y habilidades, y enfrentarlos en combates basados en turnos para ver quién emerge victorioso.

## Características

- **Simulación de Combate**: Permite simular combates entre dos equipos, cada uno compuesto por varios personajes.
- **Triángulo de Armas**: Implementa el sistema de triángulo de armas de Fire Emblem, donde ciertas armas tienen ventajas sobre otras.
- **Ataques y Contraataques**: Soporta lógicas de ataque y contraataque, incluyendo la posibilidad de un segundo ataque ("Follow-Up") si un personaje es significativamente más rápido que su oponente.
- **Ventaja Basada en Armas**: Calcula y aplica ventajas basadas en el tipo de arma utilizada por los combatientes.
- **Gestión de Personajes**: Permite a los usuarios seleccionar y configurar los personajes que participarán en el combate, incluyendo su arma, estadísticas y habilidades.

## Cómo Iniciar

1. Clona el repositorio a tu máquina local.
2. Asegúrate de tener instalado [.NET](https://dotnet.microsoft.com/) en tu sistema.
3. Navega hasta la carpeta del proyecto y ejecuta el siguiente comando para compilar el proyecto
4. Una vez compilado el proyecto, puedes ejecutar el proyecto
5. Sigue las instrucciones en la consola para configurar los equipos y comenzar la simulación de combate.

## Desarrollo

Este proyecto fue desarrollado utilizando C# y .NET. Se hizo uso de las siguientes características y patrones de diseño:

- **Clases y Objetos**: Para representar personajes, habilidades y el entorno de combate.
- **Herencia y Polimorfismo**: Utilizados para crear una jerarquía de clases de personajes y habilidades.
- **Encapsulación**: Para proteger el estado interno de los objetos y exponer solo lo necesario a través de propiedades públicas.

## Contribuir

Este proyecto es de código abierto, y las contribuciones son bienvenidas. Si deseas contribuir, por favor:

1. Haz un fork del proyecto.
2. Crea una nueva rama para tus cambios.
3. Envía un pull request con tus cambios para ser revisados.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.
