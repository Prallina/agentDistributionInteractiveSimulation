using System;

namespace AgentDistribution
{

    class AdjacencyMatrix
    {
        // 2D Array
        private int[,] _matrix;
        private int _size;
        private int numOfEdges;
        private EdgeContainer ec;

        public AdjacencyMatrix(int n)
        {
            _size = n;
            _matrix = new int[_size, _size];
            ec = EdgeContainer.Instance;
            setIDMatrix();

        }

        // sets up first half of Adjacency matrix
        private void setIDMatrix()
        {
            int _idCounter = 0;
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for (int column = 0; column < _matrix.GetLength(1); column++)
                {
                    if (column < row)
                    {
                        _matrix[row, column] = ++_idCounter;
                        ec.addEdge(_idCounter);

                    }
                    else if (row == column)
                    {
                        _matrix[row, column] = 0;
                    }
                    else
                    {
                        _matrix[row, column] = 0;
                    }
                    // Debug
                    //Console.Write(_matrix[row, column] + "\t");
                    //if (column + 1 == _size)
                    //{
                    //    Console.Write("\n");
                    //}

                }
            }

            numOfEdges = _idCounter;
            setUpSecondHalf();
        }

        // sets up second half of Adjacency matrix
        private void setUpSecondHalf()
        {
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for (int column = 0; column < _matrix.GetLength(1); column++)
                {
                    if (column > row)
                    {
                        _matrix[row, column] = _matrix[column, row];
                    }
                    // Debug
                    Console.Write(_matrix[row, column] + "\t");
                    if (column + 1 == _size)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        // returns IDs of Edges
        public int[] getEdgesIDs(int id)
        {
            int idToReturn = id - 1;
            int[] result = new int[_size - 1];
            int counter = 0;
            if (_matrix == null)
            {
                throw new ArgumentNullException("array");
            }

            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                if (_matrix[row, idToReturn] != 0)
                {
                    result[counter] = _matrix[row, idToReturn];
                    counter++;
                }
            }

            return result;
        }

        // returns ID of Nodes who are connected with Edge.ID == edgeID
        public int[] getStartEndNodes(int edgeID)
        {
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for (int column = 0; column < _matrix.GetLength(1); column++)
                {
                    if (_matrix[row, column] == edgeID)
                    {
                        int[] tmp = { row, column };
                        return tmp;
                    }

                }
            }
            return null;
        }
    }
}