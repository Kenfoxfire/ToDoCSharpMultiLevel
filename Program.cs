using ToDoApp.Model;
using ToDoApp.Service;
using static ToDoApp.Utils.Utils;
TareaService tareaService = new TareaService();
String opcion = "";

do
{
    Console.WriteLine(@"
Bienvenid@ al sistema de tareas, porfavor seleccione una opcion

    1. Crear tarea
    2. Ver tareas principales pendientes
    3. Ver tareas principales completadas
    4. Editar tarea
    5. Eliminar tarea <Si elimina una tarea principal, eliminará las subtareas>
    6. Completar tarea
    7. Salir del sistema.
    ");
    try
    {
        opcion = Console.ReadLine() ?? "";

        switch (opcion)
        {
            case "1":

                tareaService.CreateTareaMain();
                break;
            case "2":
                tareaService.MostrarTareas(TareaBase.EstadoEnum.Pendiente);
                break;
            case "3":
                tareaService.MostrarTareas(TareaBase.EstadoEnum.Completado);
                break;
            case "4":
                tareaService.EditarTarea();
                break;
            case "5":
                tareaService.EliminarTarea();
                break;
            case "6":
                tareaService.CompletarTarea();
                break;
            default:
                Log("La opcion elegida no existe", LogType.Warning);
                break;
        }
    }
    catch (IOException)
    {
        Log("Error al obtener los datos por consola", LogType.Error);
    }

}
while (!string.Equals(opcion, "7", StringComparison.OrdinalIgnoreCase));
