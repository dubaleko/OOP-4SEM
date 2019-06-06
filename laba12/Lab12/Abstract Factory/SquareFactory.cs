namespace Lab12
{
    class SquareFactory: IGeometryFactory
    {
        public IGeometryObject CreateGeometryObject()
        {
            return new Square();
        }
    }
}
