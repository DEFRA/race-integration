using RACE2.DataModel;

namespace RACE2.WebApi.GraphQL
{
    public class UserType :ObjectType<Userdetails>
    {
        protected override void Configure(IObjectTypeDescriptor<Userdetails> descriptor)
        {
            descriptor.Field(a => a.Id).Type<IdType>();
            descriptor.Field(a => a.UserName).Type<StringType>();
            descriptor.Field(a => a.NormalizedUserName).Type<StringType>();
            descriptor.Field(a => a.Email).Type<StringType>();
            descriptor.Field(a => a.NormalizedEmail).Type<StringType>();
            descriptor.Field(a => a.EmailConfirmed).Type<StringType>();
            descriptor.Field(a => a.PhoneNumber).Type<StringType>();
            descriptor.Field(a => a.c_defra_id).Type<StringType>();
            descriptor.Field(a => a.c_display_name).Type<StringType>();
            descriptor.Field(a => a.c_emergency_phone).Type<StringType>();
            descriptor.Field(a => a.c_first_name).Type<StringType>();
            
            
            
            //descriptor.Field<BlogPostResolver>(b =>
            //b.GetBlogPosts(default, default));
        }
    }
}
