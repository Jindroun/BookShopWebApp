using AutoMapper;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using DataAccessLayer.Entities;

namespace BusinessLayer;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Define your mappings here
        CreateMap<BookEntity, BookDisplay>()
        .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Author.FirstName))
        .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName))
        .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.SecondaryGenres, opt => opt.MapFrom(src => src.SecondaryGenres.Select(g => g.Name)))
        .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher.Name))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ShopItem.Price))
        .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.ShopItem.Stock));

        CreateMap<BookDto, BookEntity>();

        CreateMap<UserEntity, UserDisplay>()
          .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault()!.Street))
          .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault()!.City))
          .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault()!.PostalCode))
          .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault()!.Country))
          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AccountInfo.Email))
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AccountInfo.UserName));

        CreateMap<UserDto, UserEntity>()
            .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src =>
                src.Address == null
                    ? new List<AddressDto>()
                    : new List<AddressDto> { src.Address }
            ));

        CreateMap<AddressDto, AddressEntity>();

        CreateMap<LocalIdentityDto, LocalIdentityUser>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<LocalIdentityUser, UserDisplay>()
          .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.User.Addresses.FirstOrDefault()!.Street))
          .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.Addresses.FirstOrDefault()!.City))
          .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.User.Addresses.FirstOrDefault()!.PostalCode))
          .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User.Addresses.FirstOrDefault()!.Country))
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
          .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<RatingEntity, RatingDisplay>()
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
          .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
          .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
          .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.Id))
          .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));

        CreateMap<RatingDto, RatingEntity>();

        CreateMap<WishlistEntryEntity, WishlistEntryDisplay>()
          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
          .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
          .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
          .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.Id))
          .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));

        CreateMap<WishlistEntryDto, WishlistEntryEntity>();

        CreateMap<AddressEntity, AddressDisplay>();
        CreateMap<AddressDisplay, AddressDto>();

        CreateMap<UserEntity, UserDetailDisplay>()
         .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresses.FirstOrDefault()))
         .ForMember(dest => dest.WishlistEntries, opt => opt.MapFrom(src => src.WishlistEntries))
         .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AccountInfo.Email))
         .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AccountInfo.UserName));

        CreateMap<OrderDto, OrderEntity>()
          .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
          .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
          .ForMember(dest => dest.PlacedDate, opt => opt.MapFrom(_ => DateTime.Now))
          .ForMember(dest => dest.CouponCode, opt => opt.Ignore()); // handled in service
          // OrderItems.PricePerItem and TotalPrice are handled in service becasue of DB calls

        CreateMap<OrderItemDto, OrderItemEntity>();
        CreateMap<OrderItemEntity, OrderItemDto>();
        CreateMap<AddressDto, AddressEntity>();
        CreateMap<AddressEntity, AddressDto>();


        CreateMap<ShopItemEntity, ShopItemDisplay>()
          .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
          .ForMember(dest => dest.AuthorFirstName, opt => opt.MapFrom(src => src.Book.Author.FirstName))
          .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Book.Author.LastName))
          .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Book.Genre.Name))
          .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title))
          .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Book.Description));

        CreateMap<ShopItemDto, ShopItemEntity>();

        CreateMap<AuditLogEntity, AuditDisplay>();
        CreateMap<CartItem, OrderItemDto>();

        CreateMap<AuthorEntity, AuthorDisplay>();
        CreateMap<GenreEntity, GenreDisplay>();

        CreateMap<OrderEntity, OrderDisplay>()
          .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.User.FirstName))
          .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.User.LastName))
          .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PlacedDate))
          .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
          .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
          .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
          .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
          .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
          .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
          .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
          .ForMember(dest => dest.CouponCode, opt => opt.MapFrom(src => src.CouponCode));

        CreateMap<OrderItemEntity, OrderItemDisplay>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.ShopItem.Book.Title));

        CreateMap<BookEntity, BookDto>();
        CreateMap<PublisherEntity, PublisherDisplay>();
        CreateMap<AuthorEntity, AuthorDto>();
        CreateMap<AuthorDto, AuthorEntity>();

        CreateMap<GenreEntity, GenreDto>();
        CreateMap<GenreDto, GenreEntity>();

        CreateMap<OrderEntity, OrderDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.CouponCode, opt => opt.MapFrom(src => src.CouponCode != null? src.CouponCode.Code : null));

        CreateMap<GiftCardDto, GiftCardEntity>();
        CreateMap<GiftCardEntity, GiftCardDisplay>();
        CreateMap<GiftCardEntity, GiftCardSummary>()
          .ForMember(dest => dest.CouponCodeCount, opt => opt.MapFrom(src => src.CouponCodes.Count));

        CreateMap<CouponCodeDto, CouponCodeEntity>();
        CreateMap<CouponCodeEntity, CouponCodeDisplay>();
        CreateMap<CouponCodeEntity, CouponCodeOrderDisplay>();
        CreateMap<CouponCodeEntity, CouponCodeSummary>();
    }
}
