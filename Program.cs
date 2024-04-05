//Proyecto Battleship Edgar =============================================================

using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

//=======================================================================================
//AREA DE FUNCIONES =====================================================================
//=======================================================================================


//AREA DE TABLERO NEUTRO ==============

//Función || Generar un tablero en blanco (Lleno de el símbolo base) ====================
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
                Console.ForegroundColor = ConsoleColor.Magenta;
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

            }
            else if (eetablero[ii, jj] == 'X')
            {
                Console.ForegroundColor =  ConsoleColor.Red;
                Console.Write($"{eetablero[ii, jj],2} ");
                Console.ResetColor();

            }
            else if (eetablero[ii, jj] == 'O')
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{eetablero[ii, jj],2} ");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
                Console.Write($"{eetablero[ii, jj],2} ");
            }
        }
        Console.WriteLine($"");
    }
}



//AREA DE TABLERO ENEMIGO ================

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


//AREA DE ATAQUE ===========

//Función || Dañar tablero enemigo ======================================================
static void ffdañar_tablero_enemigo(char[,] eetablero_enemigo, char eeletra_fila, int eenumero_columna, char eebase, char eedanio, char eefallo, char[,] eetablero_propio)
{
    int eetamanio = eetablero_enemigo.GetLength(0);
    int eeletra_fila_numero = ffconvertir_letra_en_numero(eeletra_fila);
    if (eetablero_enemigo[eeletra_fila_numero,eenumero_columna] != eebase)
    {
        eetablero_enemigo[eeletra_fila_numero, eenumero_columna] = eedanio;
        eetablero_propio[eeletra_fila_numero, eenumero_columna] = eedanio;
        Console.WriteLine($"HAS ACERTADO UN GOLPE!!!");
        Console.ReadKey();
    }
    else
    {
        eetablero_propio[eeletra_fila_numero, eenumero_columna] = eefallo;
        Console.WriteLine($"HAS FALLADO EL GOLPE!!!");
        Console.ReadKey();
    }
}

//Funcion || Convertir letra en numero ==================================================
static int ffconvertir_letra_en_numero(char eeletra_fila)
{
    eeletra_fila = char.ToUpper(eeletra_fila);
    int eevalor = eeletra_fila - 'A';
    return eevalor;
}


//AREA DE COMPROBACIÓN ================

//Función || Comprobar Tablero enemigo ==================================================
static bool ffcomprobar_tablero_enemigo(char[,] eetablero_enemigo, char eedanio, char eebase)
{
    int eetamanio = eetablero_enemigo.GetLength(0);

    for (int ii = 0; ii < eetamanio; ii++)
    {
        for (int jj = 0; jj < eetamanio; jj++)
        {
            if (eetablero_enemigo[ii,jj] != eedanio && eetablero_enemigo[ii, jj] != eebase)
            {
                return true;
            } 
        }
    }
    return false;
}

//=======================================================================================
//AREA DE DEFINICIONES ==================================================================
//=======================================================================================

char eebase = '*';
char eedanio = 'O';
char eefallo = 'X';

string[] eebarcos = { "F", "NN", "PPP" };

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
                Console.WriteLine($"\nTablero vacío:");
                ffgenerar_tablero_en_blanco(eetablero, eebase);
                //ffimprimit_tablero (eetablero);
                Array.Copy(eetablero, eetablero_enemigo,eetablero.Length);
                Array.Copy(eetablero, eetablero_propio, eetablero.Length);

                foreach (string eebarco in eebarcos)
                {
                    ffcolocar_los_barcos_enemigos(eetablero_enemigo, eebarco, eebase);
                }
                Console.WriteLine($"\nTablero enemigo: (Se imprime por motivos de comprobación)");
                ffimprimit_tablero(eetablero_enemigo);
                Console.WriteLine($"\nTablero propio: \"X\" Es un golpe FALLIDO y \"O\" Es un golpe ACERTADO");
                ffimprimit_tablero(eetablero_propio);
                Console.WriteLine($"\nComienza el juego!!!");

                do
                {
                    try
                    {
                        Console.WriteLine($"");

                        Console.WriteLine($"Ingrese la fila: (Letra)");
                        char eeletra_fila = char.Parse(Console.ReadLine());
                        Console.WriteLine($"Ingrese la columna: (Numero)");
                        int eenumero_columna = int.Parse(Console.ReadLine()) - 1;

                        if (eetablero_enemigo.GetLength(0) + 1 > ffconvertir_letra_en_numero(eeletra_fila) && eetablero_enemigo.GetLength(0)+1 > eenumero_columna)
                        {
                            ffdañar_tablero_enemigo(eetablero_enemigo, eeletra_fila, eenumero_columna, eebase, eedanio, eefallo, eetablero_propio);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Los datos ingresados se salen de los parámetros, inténtelo de nuevo:");
                            Console.ReadKey();
                        }
                        Console.WriteLine($"\nTablero enemigo: (Se imprime por motivos de comprobación)");
                        ffimprimit_tablero(eetablero_enemigo);
                        Console.WriteLine($"\nTablero propio: \"X\" Es un golpe FALLIDO y \"O\" Es un golpe ACERTADO");
                        ffimprimit_tablero(eetablero_propio);
                    }
                    catch (Exception eerror)
                    {
                        Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
                    }
                } while (ffcomprobar_tablero_enemigo(eetablero_enemigo, eedanio, eebase));

                Console.WriteLine($"Ya no queda ningún barco enemigo! HAS GANADO!!!");
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


