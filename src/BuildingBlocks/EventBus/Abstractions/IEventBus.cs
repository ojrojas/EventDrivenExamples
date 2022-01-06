using EventDrivenDesign.BuildingBlocks.EventBus.Events;

namespace EventDrivenDesign.BuildingBlocks.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent integrationEvent);
        
        void Subscribe<T, TH>() where T: IntegrationEvent
            where TH: IIntegrationEventHandler<T>;
    }
}