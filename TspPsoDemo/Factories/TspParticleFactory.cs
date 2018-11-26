
namespace TspPsoDemo
{
   

    public  class TspParticleFactory:ITspParticleFactory
  {
      private readonly TspParticleOptimizer tspOptimizer;
      public TspParticleFactory(TspParticleOptimizer tspOptimizer)
      {
          this.tspOptimizer = tspOptimizer;
      }
      public ITspParticle GetTspParticle(IRoute route)
      {
          return new TspParticle(route,this.tspOptimizer);
      }
    }
}
