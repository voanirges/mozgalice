using MatricneOptimizacije.FindMinimalRoute;
using static MatricneOptimizacije.FindMinimalRoute.Models;
using Path = MatricneOptimizacije.FindMinimalRoute.Models.Path;


namespace MatrixOptimizationTest
{
    public class FindMinimalRouteTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region test methods

        [Test]
        public void MultiDimensionalCheckResult()
        {
            int[,] testArray = new int[3, 3] { { 1, 2, 3 }, { 3, 4, 5 }, { 6, 7, 8 } };
            Direction[] AllowedDirectionsForTest = [Direction.Right, Direction.Down];

            FindMinimalRoute minimalRoute = new();

            var minimalRouteResult = minimalRoute.GetMinimalRouteMultiDimensional(testArray , AllowedDirectionsForTest);

            Assert.Multiple(() =>
            {
                Assert.That(minimalRouteResult, Is.Not.Null);
                Assert.That(new RouteWithSum
                {
                    Path = new Path()
                    {
                        Points =
                              [ new Point() { X = 0, Y = 0 },
                                new Point() { X = 0, Y = 1 },
                                new Point() { X = 0, Y = 2 },
                                new Point() { X = 1, Y = 2 },
                                new Point() { X = 2, Y = 2 } ]
                    },
                    Sum = 19
                }, Is.EqualTo(minimalRouteResult));
            });
        }

        [Test]
        public void JaggedArrayCheckResult()
        {
            int[][] jaggedArray = [
                [10, 20, 30],
                [40, 50, 60],
                [70, 80, 90]
            ];
            Direction[] AllowedDirectionsForTest = [Direction.Right, Direction.Down];

            FindMinimalRoute minimalRoute = new();

            var minimalRouteResult = minimalRoute.GetMinimalRouteJagged(jaggedArray, AllowedDirectionsForTest);

            Assert.Multiple(() =>
            {
                Assert.That(minimalRouteResult, Is.Not.Null);
                Assert.That(new RouteWithSum
                {
                    Path = new Path()
                    {
                        Points =
                              [ new Point() { X = 0, Y = 0 },
                                new Point() { X = 0, Y = 1 },
                                new Point() { X = 0, Y = 2 },
                                new Point() { X = 1, Y = 2 },
                                new Point() { X = 2, Y = 2 } ]
                    },
                    Sum = 210
                }, Is.EqualTo(minimalRouteResult));
            });
        }

        [Test]
        public void GetMinimalRouteJagged()
        {
            Direction[] AllowedDirectionsForTest = [Direction.Right, Direction.Down];

            List<RouteWithSum> Paths = new();
            FindMinimalRoute minimalRoute = new();
            var minimalRouteResult = minimalRoute.GetMinimalRouteJagged(GenerateRandomMatrixJagged(), AllowedDirectionsForTest);

            Assert.That(minimalRouteResult, Is.Not.Null);
        }

        [Test]
        public void GetMinimalRouteMultiDimensional()
        {
            Direction[] AllowedDirectionsForTest = [Direction.Right, Direction.Down];

            FindMinimalRoute minimalRoute = new();
            
            var minimalRouteResult = minimalRoute.GetMinimalRouteMultiDimensional(GenerateRandomMatrix(), AllowedDirectionsForTest);

            Assert.That(minimalRouteResult, Is.Not.Null);
        }

        [Test]
        public void CalculateRoutesMultiDimensionalMatrix()
        {
            Direction[] AllowedDirectionsForTest = [Direction.Right, Direction.Down];
            FindMinimalRoute minimalRoute = new();

            var routes = minimalRoute.CalculateRoutesMultiDimensionalMatrix(10, 10, AllowedDirectionsForTest);
            foreach (var route in routes)
            {
                Console.WriteLine("**********************");
                Console.WriteLine(route.ToString());
                Console.WriteLine("**********************");
            }

            Assert.IsTrue(routes.Any());
        }

        #endregion test methods

        #region private initializers
        private int[,] GenerateRandomMatrix()
        {
            Random rnd = new();
            int x = rnd.Next(1, 10);
            int y = rnd.Next(1, 10);
            int[,] arbitraryIntegerMatrix = new int[x,y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    arbitraryIntegerMatrix[i, j] = rnd.Next(1, 10);
                    Console.WriteLine("[{0}, {1}] = {2}", i, j, arbitraryIntegerMatrix[i, j]);
                }
            }

            return arbitraryIntegerMatrix;
        }

        private int[][] GenerateRandomMatrixJagged()
        {
            Random rnd = new();
            int x = rnd.Next(1, 10);
            int y = rnd.Next(1, 10);
            int[][] arbitraryIntegerMatrix = new int[x][];
            for (int i = 0; i < arbitraryIntegerMatrix.Length; i++)
            {
                arbitraryIntegerMatrix[i] = new int[y];
                for (int j = 0; j < arbitraryIntegerMatrix[i].Length; j++)
                    arbitraryIntegerMatrix[i][j] = rnd.Next(1, 10);
            }

            return arbitraryIntegerMatrix;
        }

        #endregion
    }
}