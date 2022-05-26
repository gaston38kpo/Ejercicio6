//////////////////////////////////
//      CLASE 9 - TAREA 6       //
// ALUMNO:  GIACOBINI GASTON    //
// FECHA:   25/05/2022          //
//////////////////////////////////

Console.Title = "PoloTic Cordoba - Tarea 6 - GIACOBINI GASTÓN 2022 - v1.0";

int ROWS = 3;
int COLUMNS = 9;
int NUMBER_FOR_WHITE_SPACE = 99;

Random random = new Random();

for (int amountOfCartons = 0; amountOfCartons < 5; amountOfCartons++)
{

    int[,] carton = new int[ROWS, COLUMNS];

    int whiteSpacesPerRow = 4;

    // Genero los indices para los huecos de las primeras dos filas
    int[,] indexesPerRowForWhiteSpaces = {
        { -1, -1, -1, -1 },
        { -2, -2, -2, -2 }
    };

    for (int iRow = 0; iRow < 2; iRow++)
    {
        for (int iColumn = 0; iColumn < 4; iColumn++)
        {
            int randomNumber = random.Next(0, 9);

            bool isNumberAlreadyInRow = false;
            for (int i = 0; i < 4; i++)
            {
                if (indexesPerRowForWhiteSpaces[iRow, i] == randomNumber)
                {
                    isNumberAlreadyInRow = true;
                    break;
                }
            }

            bool rowsAreEquals = true;
            for (int i = 0; i < 4; i++)
            {
                if (indexesPerRowForWhiteSpaces[0, i] != indexesPerRowForWhiteSpaces[1, i])
                {
                    rowsAreEquals = false;
                    break;
                }
            }

            if (isNumberAlreadyInRow || rowsAreEquals)
                iColumn--;
            else
                indexesPerRowForWhiteSpaces[iRow, iColumn] = randomNumber;
        }

    }

    // Coloco los huecos generados anteriormente en las dos primeras filas
    for (int i = 0; i < 2; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            carton[i, indexesPerRowForWhiteSpaces[i, j]] = NUMBER_FOR_WHITE_SPACE;
        }
    }

    // Genero los numeros aleatorios para el carton respetando los huecos de las dos primeras filas
    for (int iRow = 0; iRow < ROWS; iRow++)
    {
        for (int iCol = 0; iCol < COLUMNS; iCol++)
        {
            int randomNumber = random.Next(1 + (iCol * 10), (iCol == COLUMNS - 1 ? 11 : 10) + (iCol * 10));

            bool isInCarton = false;
            foreach (int number in carton)
            {
                if (number == randomNumber) isInCarton = true;
            }

            if (isInCarton) iCol--;
            else if (carton[iRow, iCol] != NUMBER_FOR_WHITE_SPACE) carton[iRow, iCol] = randomNumber;

        }
    }


    // Coloco los espacios en blanco en la ultima fila
    for (int i = 0; i < COLUMNS; i++)
    {
        if (carton[0, i] != NUMBER_FOR_WHITE_SPACE && carton[1, i] != NUMBER_FOR_WHITE_SPACE)
        {

            if (whiteSpacesPerRow != 0)
            {
                carton[2, i] = NUMBER_FOR_WHITE_SPACE;
                whiteSpacesPerRow--;
            }
            else
                break;
        }
    }

    while (whiteSpacesPerRow != 0)
    {
        int randomColumn = random.Next(0, COLUMNS);
        if (
            (whiteSpacesPerRow != 0) &&
            (carton[2, randomColumn] != NUMBER_FOR_WHITE_SPACE) &&
            !(carton[0, randomColumn] == NUMBER_FOR_WHITE_SPACE && carton[1, randomColumn] == NUMBER_FOR_WHITE_SPACE))
        {
            carton[2, randomColumn] = NUMBER_FOR_WHITE_SPACE;
            whiteSpacesPerRow--;

        }

    }


    // Muestro el Carton
    for (int iRow = 0; iRow < ROWS; iRow++)
    {
        for (int iCol = 0; iCol < COLUMNS; iCol++)
        {
            Console.Write($"{ (iCol == 0 && carton[iRow, iCol] != NUMBER_FOR_WHITE_SPACE ? "0" : "") }{ (carton[iRow, iCol] != NUMBER_FOR_WHITE_SPACE ? carton[iRow, iCol] : "░░") } ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

}
