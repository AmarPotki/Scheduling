using System.Collections;
using Framework.Domain;

namespace Scheduling.Domain
{
    public class ServiceList : ValueObject, IEnumerable<Service>
    {
        private List<Service> _services { get; }

        public ServiceList(IEnumerable<Service> services)
        {
            _services = services.ToList();
        }

        protected bool EqualsCore(ServiceList other)
        {
            return _services
                .OrderBy(x => x.Id)
                .SequenceEqual(other._services.OrderBy(x => x.Id));
        }

        protected int GetHashCodeCore()
        {
            return _services.Count;
        }

        public IEnumerator<Service> GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return GetEnumerator();
        }
        public ServiceList AddService(long id)
        {
            List<Service> services = _services.ToList();
            var result = Service.Create(id);
            //todo: need to be refactored
            if (result.IsError) throw new Exception("");
                services.Add(result.Value);

            return new ServiceList(services);
        }
    }
}
