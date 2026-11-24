# Trankilandia

Trankilandia es una aplicación de escritorio para Windows que permite abrir, visualizar y navegar cómics almacenados en archivos comprimidos.

> Estado del proyecto: prototipo en desarrollo construido con WPF.

## Funcionalidades actuales

- Apertura de archivos `.cbz` y `.cbr` mediante un selector de archivos.
- Visualización de páginas con formato JPG, JPEG, PNG o WEBP.
- Navegación página a página con botones anterior y siguiente.
- Orden de las páginas a partir del nombre completo de cada archivo dentro del archivo comprimido.

## Tecnologías

- C#
- .NET 10 para Windows
- WPF
- SharpCompress 0.50.11

## Requisitos

- Windows
- Visual Studio 2022 o posterior con soporte para aplicaciones de escritorio .NET
- .NET SDK 10
- Visual Studio workload **.NET desktop development**

## Ejecución

### Desde Visual Studio

1. Abre `Trankilandia.slnx`.
2. Selecciona `Trankilandia` como proyecto de inicio.
3. Ejecuta la solución con **Iniciar** o presiona `F5`.

### Desde la terminal

```powershell
dotnet restore
dotnet run --project .\Trankilandia.csproj
```

## Uso

1. Ejecuta la aplicación.
2. Pulsa el botón de apertura.
3. Selecciona un archivo `.cbz` o `.cbr`.
4. Usa los botones laterales para avanzar o retroceder entre páginas.

Los demás formatos o archivos sin imágenes compatibles muestran un mensaje de error.

## Estructura del proyecto

```text
Trankilandia/
├── Assets/         Recursos gráficos del proyecto
├── Controls/       Controles personalizados
├── Models/         Modelos de biblioteca, metadatos y progreso
├── Resources/      Recursos adicionales
├── Services/       Servicios de la aplicación
├── ViewModels/     ViewModels para la interfaz
├── Views/          Ventanas y vistas WPF
├── App.xaml        Configuración de inicio de WPF
└── README.md
```

## Limitaciones conocidas

- La interfaz actual es una interfaz funcional básica y todavía no representa la biblioteca completa.
- El progreso de lectura no se guarda permanentemente.
- La organización, los metadatos y las etiquetas están definidos como modelos, pero aún no cuentan con una gestión completa en la interfaz.

## Próximas mejoras

- Mejorar el diseño y la navegación de la interfaz.
- Agregar biblioteca y gestión de archivos.
- Guardar progreso y configuración entre sesiones.
- Incorporar metadatos, búsqueda, etiquetas y favoritos.
- Ampliar las opciones de visualización y compatibilidad.