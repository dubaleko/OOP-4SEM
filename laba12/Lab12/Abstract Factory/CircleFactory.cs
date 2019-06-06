namespace Lab12
{
    class CircleFactory: IGeometryFactory
    {
        public IGeometryObject CreateGeometryObject()
        {
            return new Circle();
        }
    }
}
