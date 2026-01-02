namespace dragones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Obtenemos el tamaño actual de la consola
            int ancho = Console.WindowWidth;
            int alto = Console.WindowHeight;

            // Dibujamos la parte superior del marco
            Console.SetCursorPosition(0, 0);
            Console.Write('╔'); // Esquina superior izquierda
            for (int i = 1; i < ancho - 1; i++)
            {
                Console.Write('═'); // Línea horizontal
            }
            Console.Write('╗'); // Esquina superior derecha

            // Dibujamos los lados y el interior
            for (int fila = 1; fila < alto - 1; fila++)
            {
                Console.SetCursorPosition(0, fila);
                Console.Write('║'); // Borde izquierdo
                for (int col = 1; col < ancho - 1; col++)
                {
                    // Puedes escribir contenido o espacios aquí
                    Console.Write(' '); // Espacio interior
                }
                Console.SetCursorPosition(ancho - 1, fila);
                Console.Write('║'); // Borde derecho
            }

            // Dibujamos la parte inferior del marco
            Console.SetCursorPosition(0, alto - 1);
            Console.Write('╚'); // Esquina inferior izquierda
            for (int i = 1; i < ancho - 1; i++)
            {
                Console.Write('═'); // Línea horizontal
            }
            Console.Write('╝'); // Esquina inferior derecha

            // Mantenemos la consola abierta hasta que se presione una tecla
            Console.SetCursorPosition(5, alto - 2); // Mover el cursor para no sobrescribir el marco
            Console.WriteLine("Presiona cualquier tecla para salir...");

            Console.CancelKeyPress += (sender, e) =>
            {
                // Manejar la señal de interrupción (Ctrl+C)
                e.Cancel = true; // Evitar que la aplicación se cierre inmediatamente
                Console.SetCursorPosition(5, alto - 3);
                Console.WriteLine("Interrupción detectada. Saliendo...");
                Environment.Exit(0);
            };

            Console.CursorVisible = false; // Ocultar el cursor



            while (true)
            {
            }
        }
    }
}
