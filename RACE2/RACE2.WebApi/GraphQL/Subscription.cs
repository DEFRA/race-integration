using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using RACE2.DataModel;

namespace RACE2.WebApi.GraphQL
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Userdetails>>
        OnUserCreated([Service]
        ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync
            <string, Userdetails>("AuthorCreated",
            cancellationToken);
        }
        //[SubscribeAndResolve]
        //public async ValueTask<ISourceStream
        //<List<Author>>> OnAuthorsGet([Service]
        //ITopicEventReceiver eventReceiver,
        //   CancellationToken cancellationToken)
        //{
        //    return await eventReceiver.SubscribeAsync<string,
        //    List<Author>>("ReturnedAuthors",
        //    cancellationToken);
        //}
        //[SubscribeAndResolve]
        //public async ValueTask<ISourceStream<Author>>
        //OnAuthorGet([Service] ITopicEventReceiver
        //eventReceiver, CancellationToken cancellationToken)
        //{
        //    return await eventReceiver.SubscribeAsync<string,
        //    Author>("ReturnedAuthor", cancellationToken);
        //}
        //[SubscribeAndResolve]
        //public async ValueTask<ISourceStream<BlogPost>>
        //OnBlogPostsGet([Service] ITopicEventReceiver
        //eventReceiver, CancellationToken cancellationToken)
        //{
        //    return await eventReceiver.SubscribeAsync<string,
        //    BlogPost>("ReturnedBlogPosts", cancellationToken);
        //}
        //[SubscribeAndResolve]
        //public async ValueTask<ISourceStream<BlogPost>>
        //OnBlogPostGet([Service] ITopicEventReceiver
        //eventReceiver, CancellationToken cancellationToken)
        //{
        //    return await eventReceiver.SubscribeAsync<string,
        //    BlogPost>("ReturnedBlogPost", cancellationToken);
        //}
    }
}
