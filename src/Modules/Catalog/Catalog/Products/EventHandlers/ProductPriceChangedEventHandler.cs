
namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
        : INotificationHandler<ProductPriceChangedEvent>
    {
        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            // publish integration events to update basket price
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType());
            return Task.CompletedTask;
        }
    }
}
