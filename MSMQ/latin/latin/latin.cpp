#include "stdafx.h"
#include <cstdlib>

int _tmain(int argc, _TCHAR* argv[])
{
	int n = 10;

	int **matrix = new int*[n];

	for (int i = 0; i < n; ++i)
		matrix[i] = new int[n];

	for (int i = 0; i < n; ++i)
	{
		for (int j = 0; j < n; ++j)
		{
			matrix[i][j] = rand() % n;
		}
	}

	for (int i = 0; i < n; ++i)
	{
		for (int j = 0; j < n; ++j)
		{
			printf("%d ", matrix[i][j]);
		}

		printf("\n");
	}

	return 0;
}

