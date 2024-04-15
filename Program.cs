//Proyecto Battleship Edgar =============================================================

using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

//=======================================================================================
//AREA DE FUNCIONES =====================================================================
//=======================================================================================



//AREA DE TABLERO NEUTRO ==============

//Función || Generar un tablero en blanco (Lleno del símbolo base) ====================
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

//Función || Imprimir tableros, los dos: ==========================================================
static void ffimprimir_tableros(char[,] eetablero_propio, char[,] eetablero_enemigo, int eeintentos, int eepuntos)
{
    Console.WriteLine($"\nTablero enemigo: (Se imprime por motivos de comprobación)");
    ffimprimit_tablero(eetablero_enemigo);
    Console.WriteLine($"\nTablero propio: \"X\" Es un golpe FALLIDO y \"O\" Es un golpe ACERTADO");
    Console.WriteLine($"Intentos: {eeintentos} \tPuntos: {eepuntos}");
    ffimprimit_tablero(eetablero_propio);
    Console.WriteLine($"\nComienza el juego!!!");
}

//Función || Imprimir tableros en multijugador: ==========================================================
static void ffimprimir_tableros_multijugador(char[,] eetablero1, char[,] eetablero2)
{
    Console.WriteLine($"\nTablero con barcos:");
    ffimprimit_tablero(eetablero2);
    Console.WriteLine($"\nTablero de ataque:");
    ffimprimit_tablero(eetablero1);
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

                ffcolocando_barcos(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion);
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
                ffcolocando_barcos(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion);
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
                ffcolocando_barcos(eetablero_enemigo, eebarco, eefila, eecolumna, eedireccion);
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
            eecolumnatem = 0;
            eefilatem = 0;
            break;
    }
    int eefilasig = eefila + eefilatem;
    int eecolumnasig = eecolumna + eecolumnatem;

    for (int ii = 0; ii < eebarco.Length; ii++)
    {
        if (eefilasig >= eetamanio || eecolumnasig >= eetamanio || eetablero_enemigo[eefila, eecolumna] != eebase || eetablero_enemigo[eefilasig, eecolumnasig] != eebase)
        {
            return false;
        }
        eefilasig += eefilatem;
        eecolumnasig += eecolumnatem;
    }
    return true;    
}

//Función || Colocando barcos ===========================================================
static void ffcolocando_barcos(char[,] eetablero_enemigo, string eebarco, int eefila, int eecolumna, int eedireccion)
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
            eecolumnatem = 0;
            eefilatem = 0;
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

    if (eetablero_enemigo[eeletra_fila_numero,eenumero_columna] != eebase && eetablero_enemigo[eeletra_fila_numero, eenumero_columna] != eefallo)
    {
        eetablero_enemigo[eeletra_fila_numero, eenumero_columna] = eefallo;
        eetablero_propio[eeletra_fila_numero, eenumero_columna] = eedanio;
        Console.WriteLine($"HAS ACERTADO UN GOLPE!!!");
        Console.ReadKey();

    }
    else if (eetablero_propio[eeletra_fila_numero, eenumero_columna] == eedanio || eetablero_propio[eeletra_fila_numero, eenumero_columna] == eefallo)
    {
        Console.Clear();
        Console.WriteLine($"Ya has elegido esta posición anteriormente. Intenta una diferente.");
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
static bool ffcomprobar_tablero_enemigo(char[,] eetablero_enemigo, char eefallo, char eebase)
{
    int eetamanio = eetablero_enemigo.GetLength(0);

    for (int ii = 0; ii < eetamanio; ii++)
    {
        for (int jj = 0; jj < eetamanio; jj++)
        {
            if (eetablero_enemigo[ii,jj] != eefallo && eetablero_enemigo[ii, jj] != eebase)
            {
                return false;
            } 
        }
    }
    return true;
}



//AREA DE PUNTUACIÓN ==================

//Función || Intentos ===================================================================
static int ffintentos(char[,] eetablero_enemigo, char[,] eetablero_propio, char eeletra_fila, int eenumero_columna, char eebase, char eedanio,char eefallo, int eeintentos)
{
    int eetamanio = eetablero_propio.GetLength(0);
    int eeletra_fila_numero = ffconvertir_letra_en_numero(eeletra_fila);
    eeintentos = eeintentos - 1;
    if (eetablero_propio[eeletra_fila_numero, eenumero_columna] == eedanio || eetablero_propio[eeletra_fila_numero, eenumero_columna] == eefallo)
    {
        eeintentos = eeintentos + 1;
    }
    else if (eetablero_enemigo[eeletra_fila_numero, eenumero_columna] != eebase && eetablero_enemigo[eeletra_fila_numero, eenumero_columna] != eefallo )
    {
        eeintentos = eeintentos + 6;
    }
    return eeintentos;
}

//Función || Puntuación =================================================================
static int ffpuntos(char[,] eetablero_enemigo, char eeletra_fila, int eenumero_columna, char eebase,char eefallo, int eepuntos)
{
    int eeletra_fila_numero = ffconvertir_letra_en_numero(eeletra_fila);

    if (eetablero_enemigo[eeletra_fila_numero, eenumero_columna] != eebase && eetablero_enemigo[eeletra_fila_numero, eenumero_columna] != eefallo)
    {
        eepuntos = eepuntos + 100;
    }
    return eepuntos;
}


//AREA DE DOS JUGADORES =====================
//Función || Colocar los barcos manualmente ==========================
static char[,] ffcolocar_barcos_manual(char[,] eetablerotem, string eebarco, char eebase)
{
    int eetamanio = eetablerotem.GetLength(0);
    bool eecolocado = false;
    int ii = 0;

    if (eebarco == "F")
    {
        for (int jj = 4; jj != 0;)
        {
            try
            {
                Console.Clear();
                ffimprimit_tablero(eetablerotem);
                Console.WriteLine($"\nIngresando los Barcos \"{eebarco}\". Barcos por ingresar {jj}");
                Console.WriteLine($"\nIngrese la fila: (Letra)");
                char eeletra_fila = char.Parse(Console.ReadLine());
                Console.WriteLine($"Ingrese la columna: (Numero)");
                int eenumero_columna = int.Parse(Console.ReadLine()) - 1;
                int eeletra_fila_numero = ffconvertir_letra_en_numero(eeletra_fila);
                int eedireccion = 2;
                if (ffpodemos_colocar_barco(eetablerotem, eebarco, eeletra_fila_numero, eenumero_columna, eedireccion, eebase))
                {
                    ffcolocando_barcos(eetablerotem, eebarco, eeletra_fila_numero, eenumero_columna, eedireccion);
                    jj--;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No se puede colocar el BARCO en esta posición; Intenta una diferente.");
                    Console.ReadKey();
                }
            }
            catch (Exception eerror)
            {
                Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
            }
        }
    }
    else if (eebarco == "NN")
    {
        for (int jj = 3; jj != 0;)
        {
            try
            {
                Console.Clear();
                ffimprimit_tablero(eetablerotem);
                Console.WriteLine($"\nIngresando los Barcos \"{eebarco}\". Barcos por ingresar {jj}");
                Console.WriteLine($"\nIngrese la fila: (Letra)");
                char eeletra_fila = char.Parse(Console.ReadLine());
                Console.WriteLine($"Ingrese la columna: (Numero)");
                int eenumero_columna = int.Parse(Console.ReadLine()) - 1;
                int eeletra_fila_numero = ffconvertir_letra_en_numero(eeletra_fila);
                Console.WriteLine($"Ingrese la dirección: Horizontal = \"0\" -- Vartical = \"1\"");
                int eedireccion = int.Parse(Console.ReadLine());

                if (eedireccion != 0 && eedireccion != 1)
                {
                    Console.Clear();
                    Console.WriteLine($"La dirección ingresada no es valida; Inténtalo de nuevo.");
                    Console.ReadKey();
                }
                else if (ffpodemos_colocar_barco(eetablerotem, eebarco, eeletra_fila_numero, eenumero_columna, eedireccion, eebase))
                {
                    ffcolocando_barcos(eetablerotem, eebarco, eeletra_fila_numero, eenumero_columna, eedireccion);
                    jj--;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No se puede colocar el BARCO en esta posición; Intenta una diferente.");
                    Console.ReadKey();
                }
            }
            catch (Exception eerror)
            {
                Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
            }
        }
    }
    else
    {
        for (int jj = 1; jj != 0;)
        {
            try
            {
                Console.Clear();
                ffimprimit_tablero(eetablerotem);
                Console.WriteLine($"\nIngresando los Barcos \"{eebarco}\". Barcos por ingresar {jj}");
                Console.WriteLine($"\nIngrese la fila: (Letra)");
                char eeletra_fila = char.Parse(Console.ReadLine());
                Console.WriteLine($"Ingrese la columna: (Numero)");
                int eenumero_columna = int.Parse(Console.ReadLine()) - 1;
                int eeletra_fila_numero = ffconvertir_letra_en_numero(eeletra_fila);
                Console.WriteLine($"Ingrese la dirección: Horizontal = \"0\" -- Vartical = \"1\"");
                int eedireccion = int.Parse(Console.ReadLine());

                if (eedireccion != 0 && eedireccion != 1)
                {
                    Console.Clear();
                    Console.WriteLine($"La dirección ingresada no es valida; Inténtalo de nuevo.");
                    Console.ReadKey();
                }
                else if (ffpodemos_colocar_barco(eetablerotem, eebarco, eeletra_fila_numero, eenumero_columna, eedireccion, eebase))
                {
                    ffcolocando_barcos(eetablerotem, eebarco, eeletra_fila_numero, eenumero_columna, eedireccion);
                    jj--;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"No se puede colocar el BARCO en esta posición; Intenta una diferente.");
                    Console.ReadKey();
                }
            }
            catch (Exception eerror)
            {
                Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
            }
        }
    }

/*
try
{

}
catch (Exception eerror)
{
    Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
}
*/

    return eetablerotem;
}



//=======================================================================================
//AREA DE DEFINICIONES ==================================================================
//=======================================================================================

//ingreso de fila y columna=====
int eenumero_columna;
char eeletra_fila;

int eepuntos = 0;
int eeintentos = 20;

char eebase = '*';
char eedanio = 'O';
char eefallo = 'X';

string[] eebarcos = { "F", "NN", "PPP" };

char[,] eetablero = new char[20, 20]; 
char[,] eetablerotem = new char[20, 20]; //El tablero temporal ========
char[,] eetablero_enemigo = new char[20, 20];
char[,] eetablero_propio = new char[20, 20];
char[,] eetablero_enemigo2 = new char[20, 20];
char[,] eetablero_propio2 = new char[20, 20];

bool eebool = true;



//=======================================================================================
//PROGRAMA PRINCIPAL ====================================================================
//=======================================================================================
for (int ii = 0; ii != -1;)
{
    try
    {
        Console.Clear();
        Console.WriteLine($"Elija una opción:\n1. Iniciar el juego.\n2. Dos Jugadores\n\n-1. Salir del programa.");
        int eemenu1 =  int.Parse( Console.ReadLine() );

        switch (eemenu1 )
        {
            case 1:
                Console.Clear() ;
                ffgenerar_tablero_en_blanco(eetablero, eebase);
                Array.Copy(eetablero, eetablero_enemigo,eetablero.Length);
                Array.Copy(eetablero, eetablero_propio, eetablero.Length);

                foreach (string eebarco in eebarcos)
                {
                    ffcolocar_los_barcos_enemigos(eetablero_enemigo, eebarco, eebase);
                }
                ffimprimir_tableros(eetablero_propio, eetablero_enemigo, eeintentos, eepuntos);
                do
                {
                    try
                    {
                        Console.WriteLine($"\nIngrese en Fila: \"A\" y en Columna: \"-1\" Para salir de la partida.");

                        Console.WriteLine($"\nIngrese la fila: (Letra)");
                        eeletra_fila = char.Parse(Console.ReadLine());
                        Console.WriteLine($"Ingrese la columna: (Numero)");
                        eenumero_columna = int.Parse(Console.ReadLine()) - 1;
                        eebool = true;

                        if (ffconvertir_letra_en_numero(eeletra_fila) == 0 && eenumero_columna+1 == -1)
                        {
                            Console.Clear();
                            Console.WriteLine($"Has salido de la partida.");
                            eebool = false;
                            Console.ReadKey();
                        }
                        else if (eetablero_enemigo.GetLength(0) + 1 > ffconvertir_letra_en_numero(eeletra_fila) && eetablero_enemigo.GetLength(0) + 1 > eenumero_columna)
                        {
                            eeintentos = ffintentos(eetablero_enemigo, eetablero_propio, eeletra_fila, eenumero_columna, eebase, eedanio,eefallo, eeintentos);
                            eepuntos = ffpuntos(eetablero_enemigo, eeletra_fila, eenumero_columna, eebase, eefallo, eepuntos);
                            ffdañar_tablero_enemigo(eetablero_enemigo, eeletra_fila, eenumero_columna, eebase, eedanio, eefallo, eetablero_propio);

                            if (eeintentos <= 0)
                            {
                                if (eepuntos >= 100)
                                {
                                    eeintentos = eepuntos / 50;
                                    Console.WriteLine($"Gracias a los puntos que habías obtenido, has ganado \"{eeintentos}\" intentos extras. Sigue jugando.");
                                    Console.ReadKey();
                                    eepuntos = 0;
                                }
                                else
                                {
                                    Console.WriteLine($"Te has quedado sin intentos. HAS PERDIDO!!!");
                                    Console.ReadKey();
                                    eebool = false;
                                }
                            }
                            else if (ffcomprobar_tablero_enemigo(eetablero_enemigo, eefallo, eebase))
                            {
                                Console.Clear();
                                Console.WriteLine($"Ya no queda ningún barco enemigo! HAS GANADO!!!");
                                Console.WriteLine($"\nTablero enemigo:");
                                ffimprimit_tablero(eetablero_enemigo);
                                eebool = false;
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Los datos ingresados se salen de los parámetros, inténtelo de nuevo:");
                            Console.ReadKey();
                        }

                        Console.Clear();
                        ffimprimir_tableros(eetablero_propio, eetablero_enemigo, eeintentos, eepuntos);

                    }
                    catch (Exception eerror)
                    {
                        Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
                    }
                } while (eebool);
                break;
            case 2:
                ffgenerar_tablero_en_blanco(eetablero, eebase);
                Array.Copy(eetablero, eetablerotem, eetablero.Length);

                Console.Clear();
                Console.WriteLine($"JUGADOR 1");
                Console.WriteLine($"Debes colocar los barcos en tu tablero.");
                Console.ReadKey();
                foreach (string eebarco in eebarcos)
                {
                    ffcolocar_barcos_manual(eetablerotem, eebarco, eebase);
                    Array.Copy(eetablerotem, eetablerotem, eetablerotem.Length);
                }
                Array.Copy(eetablerotem, eetablero_propio2, eetablerotem.Length);
                Array.Copy(eetablero, eetablero_propio, eetablero.Length);
                Console.Clear();
                ffimprimir_tableros_multijugador(eetablero_propio,eetablero_propio2);
                Console.WriteLine($"(Tableros del JUGADOR 1)");
                Console.ReadKey();

                Console.Clear();
                Console.WriteLine($"JUGADOR 2");
                Console.WriteLine($"Debes colocar los barcos en tu tablero.");
                Console.ReadKey();
                Array.Copy(eetablero, eetablerotem, eetablero.Length);
                foreach (string eebarco in eebarcos)
                {
                    ffcolocar_barcos_manual(eetablerotem, eebarco, eebase);
                    Array.Copy(eetablerotem, eetablerotem, eetablerotem.Length);
                }
                Array.Copy(eetablerotem, eetablero_enemigo2, eetablerotem.Length);
                Array.Copy(eetablero, eetablero_enemigo, eetablero.Length);
                Console.Clear();
                ffimprimir_tableros_multijugador(eetablero_enemigo, eetablero_enemigo2);
                Console.WriteLine($"(Tableros del JUGADOR 2)");

                Console.ReadKey();

                Console.Clear();
                Console.WriteLine($"INICIA EL JUEGO!!!");
                Console.ReadKey();
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine($"Turno del JUGADOR 1.");
                        Console.ReadKey();

                        ffimprimir_tableros_multijugador(eetablero_propio, eetablero_propio2);
                        Console.WriteLine($"JUGADOR 1");
                        Console.WriteLine($"\nIngrese en Fila: \"A\" y en Columna: \"-1\" Para salir de la partida.");

                        Console.WriteLine($"\nIngrese la fila: (Letra)");
                        eeletra_fila = char.Parse(Console.ReadLine());
                        Console.WriteLine($"Ingrese la columna: (Numero)");
                        eenumero_columna = int.Parse(Console.ReadLine()) - 1;
                        eebool = true;
                        if (ffconvertir_letra_en_numero(eeletra_fila) == 0 && eenumero_columna + 1 == -1)
                        {
                            Console.Clear();
                            Console.WriteLine($"Has salido de la partida.");
                            eebool = false;
                            Console.ReadKey();
                        }
                        else if (eetablero.GetLength(0) + 1 > ffconvertir_letra_en_numero(eeletra_fila) && eetablero.GetLength(0) + 1 > eenumero_columna)
                        {
                            ffdañar_tablero_enemigo(eetablero_enemigo2, eeletra_fila, eenumero_columna, eebase, eedanio, eefallo, eetablero_propio);

                            if (ffcomprobar_tablero_enemigo(eetablero_enemigo2, eefallo, eebase))
                            {
                                Console.Clear();
                                Console.WriteLine($"JUGADOR 1, HAS GANADO!!!\nEl rival se a quedado sin barcos.\nTablero JUGADOR 2:");
                                ffimprimit_tablero(eetablero_enemigo2);
                                Console.ReadKey();
                                eebool = false;
                            }
                        }

                        if (eebool == true)
                        {
                            for (int jj = 0; jj!=-1;)
                            {
                                try
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Turno del JUGADOR 2.");
                                    Console.ReadKey();

                                    ffimprimir_tableros_multijugador(eetablero_enemigo, eetablero_enemigo2);
                                    Console.WriteLine($"JUGADOR 2");
                                    Console.WriteLine($"\nIngrese en Fila: \"A\" y en Columna: \"-1\" Para salir de la partida.");

                                    Console.WriteLine($"\nIngrese la fila: (Letra)");
                                    eeletra_fila = char.Parse(Console.ReadLine());
                                    Console.WriteLine($"Ingrese la columna: (Numero)");
                                    eenumero_columna = int.Parse(Console.ReadLine()) - 1;
                                    eebool = true;
                                    if (ffconvertir_letra_en_numero(eeletra_fila) == 0 && eenumero_columna + 1 == -1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Has salido de la partida.");
                                        jj = -1;
                                        eebool = false;
                                        Console.ReadKey();
                                    }
                                    else if (eetablero.GetLength(0) + 1 > ffconvertir_letra_en_numero(eeletra_fila) && eetablero.GetLength(0) + 1 > eenumero_columna)
                                    {
                                        ffdañar_tablero_enemigo(eetablero_propio2, eeletra_fila, eenumero_columna, eebase, eedanio, eefallo, eetablero_enemigo);

                                        if (ffcomprobar_tablero_enemigo(eetablero_propio2, eefallo, eebase))
                                        {
                                            Console.Clear();
                                            Console.WriteLine($"JUGADOR 2, HAS GANADO!!!\nEl rival se a quedado sin barcos.\nTablero JUGADOR 1:");
                                            ffimprimit_tablero(eetablero_propio2);
                                            Console.ReadKey();
                                            eebool = false;
                                        }
                                        jj = -1;
                                    }
                                }
                                catch (Exception eerror)
                                {
                                    Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
                                }
                            }
                        }
                    }
                    catch (Exception eerror)
                    {
                        Console.WriteLine($"Ah ocurrido un error en el ingreso de datos; Error: \"{eerror}\"");
                    }
                } while (eebool);
                break;
            case -1:
                ii = -1;
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
        Console.ReadKey();

    }
}



//=======================================================================================
//=======================================================================================
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



//=======================================================================================
//=======================================================================================



/*
Console.WriteLine($"Hola, Soy Batman 1");
*/



//=======================================================================================
//=======================================================================================



