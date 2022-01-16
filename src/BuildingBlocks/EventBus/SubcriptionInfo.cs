using EventDrivenDesign.BuildingBlocks.EventBus.Interfaces;

namespace EventDrivenDesign.BuildingBlocks.EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubcriptionsManager
    {
        public class SubscriptionInfo
        {
            public Type HandlerType { get; }

            private SubscriptionInfo(Type handlerType)
            {
                this.HandlerType = handlerType;
            }

            public static SubscriptionInfo Typed(Type handlerType) => new SubscriptionInfo(handlerType);

        }
    }
}