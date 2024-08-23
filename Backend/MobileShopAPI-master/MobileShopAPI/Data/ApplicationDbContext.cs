using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MobileShopAPI.Models;
using System.Reflection.Emit;

namespace MobileShopAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public virtual DbSet<ActiveSubscription> ActiveSubscriptions { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CoinAction> CoinActions { get; set; } = null!;
        public virtual DbSet<CoinPackage> CoinPackages { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Evidence> Evidences { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<InternalTransaction> InternalTransactions { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductOrder> ProductOrders { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportCategory> ReportCategories { get; set; } = null!;
        public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<SubscriptionPackage> SubscriptionPackages { get; set; } = null!;
        public virtual DbSet<VnpTransaction> Transactions { get; set; } = null!;
        public virtual DbSet<UserRating> UserRatings { get; set; } = null!;

        public virtual DbSet<MarkedProduct> MarkedProducts{ get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((0))");
                entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<ActiveSubscription>(entity =>
            {
                entity.ToTable("active_subscription");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("activatedDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.SpId).HasColumnName("sp_id");

                entity.Property(e => e.UsedPost)
                    .HasColumnName("usedPost")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Số lượng bài đăng đã sử dụng");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.HasOne(d => d.Sp)
                    .WithMany(p => p.ActiveSubscriptions)
                    .HasForeignKey(d => d.SpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_active_subcription_subscription_package");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ActiveSubscriptions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_active_subcription_AspNetUsers");
            });



            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasComment("url hình ảnh");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasComment("url hình ảnh");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CoinAction>(entity =>
            {
                entity.ToTable("coin_action");

                entity.HasComment("Actions on website using coin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionName)
                    .HasMaxLength(255)
                    .HasColumnName("action_name");

                entity.Property(e => e.CaCoinAmount).HasColumnName("ca_coin_amount");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CoinPackage>(entity =>
            {
                entity.ToTable("coin_package");

                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("id");

                entity.Property(e => e.CoinAmount).HasColumnName("coin_amount");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.PackageName)
                    .HasMaxLength(255)
                    .HasColumnName("package_name");

                entity.Property(e => e.PackageValue).HasColumnName("package_value");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.ValueUnit)
                    .HasMaxLength(10)
                    .HasColumnName("value_unit")
                    .HasDefaultValueSql("('VND')")
                    .HasComment("vnđ,...v.v");
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("color");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ColorName)
                    .HasMaxLength(255)
                    .HasColumnName("colorName");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.HexValue).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<Evidence>(entity =>
            {
                entity.ToTable("evidence");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ImageUrl).HasColumnName("image_url");

                entity.Property(e => e.ReportId).HasColumnName("reportId");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Evidences)
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_evidence_Report");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.HasIndex(e => e.ProductId, "IX_image_ProductId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IsCover)
                    .HasColumnName("isCover")
                    .HasComment("hình ảnh là ảnh bìa");

                entity.Property(e => e.ProductId).HasDefaultValueSql("(CONVERT([bigint],(0)))");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasComment("url hình ảnh");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_images_product");
            });

            modelBuilder.Entity<InternalTransaction>(entity =>
            {
                entity.ToTable("internal_transaction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoinActionId).HasColumnName("coinActionId");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ItAmount).HasColumnName("it_amount");

                entity.Property(e => e.ItInfo).HasColumnName("it_info");

                entity.Property(e => e.ItSecureHash)
                    .HasColumnType("datetime")
                    .HasColumnName("it_secureHash");

                entity.Property(e => e.SpId).HasColumnName("spId");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.HasOne(d => d.CoinAction)
                    .WithMany(p => p.InternalTransactions)
                    .HasForeignKey(d => d.CoinActionId)
                    .HasConstraintName("fk_internal_transaction_coin_action");

                entity.HasOne(d => d.Sp)
                    .WithMany(p => p.InternalTransactions)
                    .HasForeignKey(d => d.SpId)
                    .HasConstraintName("fk_internal_transaction_subscription_package");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InternalTransactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_internal_transaction_AspNetUsers");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.UserId, "IX_order_userId");

                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phoneNumber");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("1 = trả hết, 2 = đặt cọc");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UserFullName)
                    .HasMaxLength(255)
                    .HasColumnName("userFullName");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_AspNetUsers");
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.BrandId, "IX_product_brandId");

                entity.HasIndex(e => e.CategoryId, "IX_product_categoryId");

                entity.HasIndex(e => e.ColorId, "IX_product_colorId");

                entity.HasIndex(e => e.SizeId, "IX_product_sizeId");

                entity.HasIndex(e => e.UserId, "IX_product_userId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.ColorId)
                    .HasColumnName("colorId")
                    .HasComment("part of primaryKey");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Priorities)
                    .HasColumnName("priorities")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SizeId)
                    .HasColumnName("sizeId")
                    .HasComment("part of primaryKey");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_catagory");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_color");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_size");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_AspNetUsers");

                entity.Property(d => d.isHidden).HasDefaultValue(true);
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.ToTable("product_order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(400)
                    .HasColumnName("orderId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_order_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_order_product");
                

            });

            


            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report");

                entity.Property(e => e.Body).HasColumnName("body");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ReportCategoryId).HasColumnName("reportCategoryId");

                entity.Property(e => e.ReportedProductId).HasColumnName("reportedProductId");

                entity.Property(e => e.ReportedUserId)
                    .HasMaxLength(450)
                    .HasColumnName("reportedUserId");

                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .HasColumnName("subject");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId")
                    .HasComment("user id of user who sent the report");

                entity.HasOne(d => d.ReportCategory)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ReportCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Report_report_category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Report_AspNetUsers");
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ReportCategory>(entity =>
            {
                entity.ToTable("report_category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");
            });

            modelBuilder.Entity<ShippingAddress>(entity =>
            {
                entity.ToTable("shipping_address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressDetail)
                    .HasMaxLength(100)
                    .HasColumnName("address_detail")
                    .HasComment("house number, district name");

                entity.Property(e => e.AddressName)
                    .HasMaxLength(255)
                    .HasColumnName("address_name");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("isDefault")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedDate");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");

                entity.Property(e => e.WardId).HasColumnName("wardId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShippingAddresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_shipping_address_AspNetUsers");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("size");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.SizeName)
                    .HasMaxLength(255)
                    .HasColumnName("sizeName");
            });

            modelBuilder.Entity<SubscriptionPackage>(entity =>
            {
                entity.ToTable("subscription_package");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.ExpiredIn)
                    .HasColumnName("expiredIn")
                    .HasComment("Số ngày sử dụng");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PostAmout)
                    .HasColumnName("postAmout")
                    .HasComment("Số lượng được tin đăng khi mua gói");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });


            modelBuilder.Entity<VnpTransaction>(entity =>
            {
                entity.ToTable("vnp_transaction");

                entity.HasIndex(e => e.OrderId, "IX_transaction_orderId");

                entity.HasIndex(e => e.UserId, "IX_transaction_userId");

                entity.Property(e => e.Id)
                    .HasMaxLength(400)
                    .HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(400)
                    .HasColumnName("orderId");

                entity.Property(e => e.PackageId)
                    .HasMaxLength(400)
                    .HasColumnName("packageId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.VnpAmount).HasColumnName("vnp_Amount");

                entity.Property(e => e.VnpBankCode)
                    .HasMaxLength(40)
                    .HasColumnName("vnp_BankCode");

                entity.Property(e => e.VnpCommand)
                    .HasMaxLength(30)
                    .HasColumnName("vnp_Command");

                entity.Property(e => e.VnpCreateDate).HasColumnName("vnp_CreateDate");

                entity.Property(e => e.VnpCurrCode)
                    .HasMaxLength(5)
                    .HasColumnName("vnp_CurrCode");

                entity.Property(e => e.VnpIpAddr)
                    .HasMaxLength(60)
                    .HasColumnName("vnp_IpAddr");

                entity.Property(e => e.VnpLocale)
                    .HasMaxLength(8)
                    .HasColumnName("vnp_Locale");

                entity.Property(e => e.VnpOrderInfo).HasColumnName("vnp_OrderInfo");

                entity.Property(e => e.VnpOrderType)
                    .HasMaxLength(100)
                    .HasColumnName("vnp_OrderType");

                entity.Property(e => e.VnpSecureHash).HasColumnName("vnp_SecureHash");

                entity.Property(e => e.VnpTmnCode)
                    .HasMaxLength(15)
                    .HasColumnName("vnp_TmnCode");

                entity.Property(e => e.VnpTxnRef)
                    .HasMaxLength(120)
                    .HasColumnName("vnp_TxnRef");

                entity.Property(e => e.VnpVersion)
                    .HasMaxLength(15)
                    .HasColumnName("vnp_Version");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_transaction_order")
                    .IsRequired(false);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("fk_vnp_transaction_coin_package")
                    .IsRequired(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transaction_AspNetUsers");
            });

            modelBuilder.Entity<UserRating>(entity =>
            {
                entity.ToTable("user_rating");

                entity.HasIndex(e => e.ProductId, "IX_user_rating_productId");

                entity.HasIndex(e => e.UsderId, "IX_user_rating_usderId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((1))")
                    .HasComment("1,2,3,4,5");

                entity.Property(e => e.UsderId).HasColumnName("usderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_rating_product");

                entity.HasOne(d => d.Usder)
                    .WithMany(p => p.UserRatings)
                    .HasForeignKey(d => d.UsderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_rating_AspNetUsers");
            });

            modelBuilder.Entity<MarkedProduct>(entity =>
            {
                entity.ToTable("marked_product");

                entity.Property(e => e.Id).HasColumnName("id");


                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("userId");
                entity.Property(e => e.ProductId)
                    .HasColumnName("productId");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.MarkedProducts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MarkedProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
