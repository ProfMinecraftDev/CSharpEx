# 🚀 CSharpEx

**El Toolkit de extensiones definitivo para .NET.** Una librería diseñada para potenciar el desarrollo multiplataforma, simplificando tareas complejas de sistema y ofreciendo azúcar sintáctico para el día a día.

## 🌟 Características Destacadas

### 🛡️ Gestión de Procesos Avanzada (`ProcessExtensions`)

Control total sobre los subprocesos de tu aplicación.

* **Job Objects (Kill-on-Close):** Asegura que los procesos hijos mueran si tu app principal se cierra.
* **RAM Limiting:** Restringe el *Working Set* (RAM física) de cualquier proceso en Windows y otras plataformas.
* **Static Factories:** Métodos estáticos para crear e iniciar procesos configurados en una sola línea.

### 🧪 Utilidades de Colecciones (`EnumerableExtensions`)

Operaciones fluidas para tus listas y enumerables.

* **PickRandom:** Selecciona un elemento al azar de cualquier colección.
* **Safe Checks:** Verifica si una lista es nula o está vacía con `IsNullOrEmpty()`.
* **AddIfMissing:** Añade elementos a una lista evitando duplicados automáticamente.

### 🛠️ Herramientas de Sistema y Datos

* **Formato de Archivos:** Convierte bytes a strings legibles (KB, MB, GB... hasta Exabytes).
* **Guard Pattern:** Validaciones rápidas como `IsBlank`, `HasContent` y `Clamp` para números.
* **Fluent Casting:** Casteo de objetos limpio con `.To<T>()` y `.As<T>()`.
* **Typed Events:** Delegado `TypedEventHandler<TSender, TArgs>` para eventos fuertemente tipados.

---

## 💻 Ejemplos de Código

### Iniciar un Servidor con Protección y Límite de RAM

Ideal para herramientas de administración de servidores (Minecraft, bases de datos, etc.).

```csharp
using CSharpEx;

// Inicia un proceso que se cerrará automáticamente al cerrar tu app
var server = ProcessExtensions.StartAsJob("java.exe", "-Xmx2G -jar server.jar");

// O inicia un proceso limitando su consumo de RAM física a 1GB
bool ok = myProcess.StartWithRamLimited(1024);

```

### Manipulación de Datos

```csharp
// Obtener un jugador aleatorio de una lista
var randomPlayer = players.PickRandom();

// Validar que un valor de configuración esté dentro de un rango
int tickRate = configValue.Clamp(1, 20);

// Mostrar el tamaño de un mundo de forma legible
long folderSize = 1536782336;
Console.WriteLine(folderSize.ToSizeString()); // Output: "1.43 GB"

```

---

## 📦 Estructura del Proyecto

* **`CSharpEx`**: Extensiones de tipos base (`Process`, `String`, `Object`, `Number`).
* **`CSharpEx.Foundation`**: Tipos base y delegados como `TypedEventHandler`.
* **`CSharpEx.Versioning`**: Polyfills para compatibilidad de atributos de plataforma en versiones antiguas de .NET.

---

## 🏗️ Compatibilidad

Gracias a nuestro sistema de preprocesamiento, la librería funciona perfectamente en:

* **.NET 3.5** (Legacy Support)
* **.NET Framework 4.6.2 / 4.8**
* **.NET 6.0 / 8.0** (Modern .NET)

Creado por **Prof Minecraft**. [Licencia MIT](LICENSE.txt).