//////////////////////////////////
//      CLASE 9 - TAREA 6       //
// ALUMNO:  GIACOBINI GASTON    //
// FECHA:   25/05/2022          //
//////////////////////////////////

Console.Title = "PoloTic Cordoba - Tarea 6 - GIACOBINI GASTÓN 2022 - v1.1";

int ROWS = 3;
int COLUMNS = 9;
int WHITESPACE = 99;

Random random = new Random();

for (int amountOfCartons = 0; amountOfCartons < 4; amountOfCartons++)
{

    int[,] carton = new int[ROWS, COLUMNS];

    // Generador de indices para cada hueco de las primeras dos filas
    int[,] whitespacesIndexesPerRow = {
        { -1, -1, -1, -1 },
        { -2, -2, -2, -2 }
    };

    for (int iRow = 0; iRow < 2; iRow++)
    {
        for (int iColumn = 0; iColumn < 4; iColumn++)
        {
            int randomIndex = random.Next(0, 9);

            bool isRandomIndexAlreadyInRow = false;
            bool firstAndSecondRowsAreEquals = true;
            for (int iColumnToQuickLook = 0; iColumnToQuickLook < 4; iColumnToQuickLook++)
            {
                if (whitespacesIndexesPerRow[iRow, iColumnToQuickLook] == randomIndex)
                    isRandomIndexAlreadyInRow = true;

                if (whitespacesIndexesPerRow[0, iColumnToQuickLook] != whitespacesIndexesPerRow[1, iColumnToQuickLook])
                    firstAndSecondRowsAreEquals = false;
            }

            if (isRandomIndexAlreadyInRow || firstAndSecondRowsAreEquals)
                iColumn--;
            else
                whitespacesIndexesPerRow[iRow, iColumn] = randomIndex;
        }
    }

    // Coloco los huecos generados anteriormente en las dos primeras filas
    for (int i = 0; i < 2; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            carton[i, whitespacesIndexesPerRow[i, j]] = WHITESPACE;
        }
    }

    // Coloco los huecos en la ultima fila
    // --Detecta dos numeros consecutivos en columna y coloca un hueco 
    int whiteSpacesPerRow = 4;

    for (int iColumn = 0; iColumn < COLUMNS; iColumn++)
    {
        if (carton[0, iColumn] != WHITESPACE && carton[1, iColumn] != WHITESPACE)
        {
            if (whiteSpacesPerRow == 0)
                break;

            carton[2, iColumn] = WHITESPACE;
            whiteSpacesPerRow--;
        }
    }

    // --Si sobraron huecos del paso anterior, coloco huecos moviendome de manera aleatoria por las columnas
    while (whiteSpacesPerRow > 0)
    {
        int randomColumn = random.Next(0, COLUMNS);

        if ((whiteSpacesPerRow != 0) && // Si sobran huecos y 
            (carton[2, randomColumn] != WHITESPACE) && // No estoy pisando otro hueco y
            !(carton[0, randomColumn] == WHITESPACE && carton[1, randomColumn] == WHITESPACE)) // Sus dos casillas superiores no son huecos
        {
            carton[2, randomColumn] = WHITESPACE;
            whiteSpacesPerRow--;
        }
    }

    // Genero los numeros aleatorios para el carton respetando los huecos de las dos primeras filas
    for (int iRow = 0; iRow < ROWS; iRow++)
    {
        for (int iColumn = 0; iColumn < COLUMNS; iColumn++)
        {
            // Genero un numero aleatorio entre el rango de la columna actual
            int randomNumber = random.Next(
                ( 1 + (iColumn * 10) ), // Min: 1 -> 11 -> 21 -> 31 -> ... -> 70 -> 80 
                ( ((iColumn == (COLUMNS - 1)) ? 11 : 10) + (iColumn * 10) ) // Max 10 -> 20 -> 30 .. -> 80 -> 91 (11 + (8 * 10))
                ); 

            bool isRandomNumberInCarton = false;
            foreach (int number in carton)
            {
                if (number == randomNumber)
                    isRandomNumberInCarton = true;
            }

            if (isRandomNumberInCarton)
                iColumn--;
            else if (carton[iRow, iColumn] != WHITESPACE)
                carton[iRow, iColumn] = randomNumber;
        }
    }

    // Muestro el Carton
    for (int iRow = 0; iRow < ROWS; iRow++)
    {
        for (int iColumn = 0; iColumn < COLUMNS; iColumn++)
        {
            Console.Write($"{ (iColumn == 0 && carton[iRow, iColumn] != WHITESPACE ? "0" : "") }{ (carton[iRow, iColumn] != WHITESPACE ? carton[iRow, iColumn] : "░░") } ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

}
