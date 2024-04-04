//Proyecto Battleship Edgar =============================================================


//=======================================================================================
//AREA DE FUNCIONES =====================================================================
//=======================================================================================

//Función || Generar un tablero en blanco (Lleno de el símbolo base) ================================
using System;
using System.Runtime.CompilerServices;

static char[,] ffgenerar_tablero_en_blanco(char[,] eetablero, char eebase)
{
    int eetablero_dimenciones = eetablero.GetLength(0);
    for (int ii = 0; ii < eetablero_dimenciones; ii++)
    {
        for (int jj = 0; jj < eetablero_dimenciones; jj++)
        {
            eetablero[ii, jj] = eebase;
        }
    }
    return eetablero;
}

//Función || Imprimir tablero: ==========================================================
static void ffimprimit_tablero(char[,] eetablero)
{
    int eetablero_dimenciones = eetablero.GetLength(0);
    int eevariable = 0;
    //Console.WriteLine($" { new string ('_', eetablero_dimenciones*2+1)}");
    Console.Write(" ");
    for (int ii = 0; ii < eetablero_dimenciones; ii++)
    {
        Console.Write($"{1+ii,3}");
    }
    Console.WriteLine($"");

    Console.Write($" {new string('-', eetablero_dimenciones * 3)}");

    Console.WriteLine($"");
    for ( int ii = 0; ii < eetablero_dimenciones; ii++)
    {
        char eeletra = Convert.ToChar ('A' + ii);
        Console.Write($"{eeletra}|");
        for (int jj = 0; jj < eetablero_dimenciones; jj++)
        {
            if (eetablero[ii, jj] == 'F')
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{eetablero[ii, jj],2} ");
                Console.ResetColor();

            }
            else if (eetablero[ii, jj] == 'N')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{eetablero[ii, jj],2} ");
                Console.ResetColor();

            }
            else if (eetablero[ii, jj] == 'P')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{eetablero[ii, jj],2} ");
                Console.ResetColor();

            } else
            {
                Console.ResetColor();
                Console.Write($"{eetablero[ii, jj],2} ");
            }
        }
        Console.WriteLine($"");
    }
}



//Función || Colocar los barcos enemigos ================================================
static void ffcolocar_los_barcos_enemigos(char[,] eetablero_enemigo, string eebarco, char eebase)
{
    int eetamanio = eetablero_enemigo.GetLength(0);
    Random eerandom = new Random();
    bool eecolocado = false;

    if (eebarco == "F")
    {
        for (int jj = 0; jj < 4;)
        {
            int eefila = eerandom.Next(eetamanio);
            int eecolumna = eerandom.Next(eetamanio);
            int eedireccion = eerandom.Next(2); // 0: horizontal, 1: vertical
            //Se puede colocar el barco?
            if (ffpodemos_colocar_barco(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion, eebase))
            {

                ffcolocando_barcos(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion, eebase);
                jj++;
            }
        }
    }
    else if (eebarco == "NN")
    {
        for (int jj = 0; jj < 3;)
        {
            int eefila = eerandom.Next(eetamanio);
            int eecolumna = eerandom.Next(eetamanio);
            int eedireccion = eerandom.Next(2); // 0: horizontal, 1: vertical

            //Se puede colocar el barco?
            if (ffpodemos_colocar_barco(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion, eebase))
            {
                ffcolocando_barcos(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion, eebase);
                jj++;
            }
        }
    }
    else
    {
        for (int jj = 0; jj < 1;)
        {
            int eefila = eerandom.Next(eetamanio);
            int eecolumna = eerandom.Next(eetamanio);
            int eedireccion = eerandom.Next(2); // 0: horizontal, 1: vertical

            //Se puede colocar el barco?
            if (ffpodemos_colocar_barco(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion, eebase))
            {
                ffcolocando_barcos(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion, eebase);
                jj++;
            }
        }
    }
}

//Función || Podemos colocar barco? ================================================
static bool ffpodemos_colocar_barco(char[,] eetablero_enemigo, string eebarco, int eefila, int eecolumna, int eedireccion, char eebase)
{
    bool eevariablebool;
    int eetamanio = eetablero_enemigo.GetLength(0);
    int eefilatem = 0;
    int eecolumnatem = 0;

    switch (eedireccion)
    {
        case 0: //horizontal
            eecolumnatem = 1;
            break;
        case 1://vertical
            eefilatem = 1;
            break;
        case 2:
            Console.WriteLine($"Ella no te ama :)");
            Console.ReadKey();
            break;
    }
    int eefilasig = eefila + eefilatem;
    int eecolumnasig = eecolumna + eecolumnatem;

    for (int ii = 0; ii < eebarco.Length; ii++)
    {
        if (eefilasig >= eetamanio || eecolumnasig >= eetamanio || eetablero_enemigo[eefilasig, eecolumnasig] != eebase)
        {
            return false;
        }
        eefilasig += eefilatem;
        eecolumnasig += eecolumnatem;
    }
    return true;
    Console.ReadKey();
    
}

//Función || Colocando barcos ===========================================================
static void ffcolocando_barcos(char[,] eetablero_enemigo, string eebarco, int eefila, int eecolumna, int eedireccion, char eebase)
{
    int eefilatem = 0;
    int eecolumnatem = 0;

    switch (eedireccion)
    {
        case 0: //horizontal
            eecolumnatem = 1;
            break;
        case 1://vertical
            eefilatem = 1;
            break;
        case 2:
            Console.WriteLine($"Ella no te ama :)");
            Console.ReadKey();
            break;
    }
    for (int ii = 0; ii < eebarco.Length;ii++)
    {
        eetablero_enemigo[eefila, eecolumna] = eebarco[ii];
        eefila += eefilatem;
        eecolumna += eecolumnatem;
    }
}


//=======================================================================================
//AREA DE DEFINICIONES ==================================================================
//=======================================================================================

char eebase = '*';

string[] eebarcos = { "F", "NN", "PPP" };

string eefragata = "F";
string eenavio = "NN";
string eeportaviones = "PPP";


char[,] eetablero = new char[20, 20];
char[,] eetablero_enemigo = new char[20, 20];
char[,] eetablero_propio = new char[20, 20];


//=======================================================================================
//PROGRAMA PRINCIPAL ====================================================================
//=======================================================================================
for (int ii = 0; ii != 2;)
{
    try
    {
        Console.Clear();
        Console.WriteLine($"Elija una opción:\n1. Iniciar el juego.\n2. Salir del programa.");
        int eemenu1 =  int.Parse( Console.ReadLine() );
        switch (eemenu1 )
        {
            case 1:
                Console.Clear() ;
                ffgenerar_tablero_en_blanco (eetablero, eebase);
                ffimprimit_tablero (eetablero);
                Array.Copy(eetablero, eetablero_enemigo,eetablero.Length);
                Array.Copy(eetablero, eetablero_propio, eetablero.Length);

                foreach (string eebarco in eebarcos)
                {
                    ffcolocar_los_barcos_enemigos(eetablero_enemigo, eebarco, eebase);
                }

                Console.WriteLine($"\nTablero enemigo:\n");
                ffimprimit_tablero(eetablero_enemigo);
                Console.ReadKey();

                break;
            case 2:
                ii = 2;
                break;
            default:
                Console.Clear() ;
                Console.WriteLine($"El carácter ingresado no es valido. Inténtelo de nuevo.");
                Console.ReadKey();
                break;
        }
    }
    catch (Exception eerror)
    {
        Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
    }
}



//=======================================================================================
//=======================================================================================
//=======================================================================================

/*

try
{

}
catch (Exception eerror)
{
    Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
}

*/


