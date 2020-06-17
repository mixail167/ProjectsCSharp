namespace WindowsFormsApp1
{
    enum CellValues
    {
        StartPosition = 0,
        Empty = -1,
        Destination = -2,
        Outside = -3,
        Barrier = -4        
    }

    static class CellValuesEx
    {
        public static int ToInt(this CellValues cellValues)
        {
            return (int)cellValues;
        }
    }
}
