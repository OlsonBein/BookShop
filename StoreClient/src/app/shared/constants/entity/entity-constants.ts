export class EntityConstants {
  public static readonly productTypes = ['Book', 'Newspaper', 'Journal'];
  public static readonly currency = ['USD', 'EUR', 'GBP', 'CHF', 'JPY', 'UAH'];
  public static readonly userState = ['Active', 'Blocked'];
  public static readonly orderStatus = ['Unpaid', 'Paid'];

  public static readonly pageSizeOptions = [5, 10, 15];
  public static readonly mainPageSizeOptions = [3, 6];

  public static readonly authorTableColumns = ['id', 'name', 'products', 'features'];
  public static readonly orderTableColumns =
    ['order', 'creationDate', 'userName', 'userEmail', 'product', 'title', 'quantity', 'amount', 'orderStatus'];
  public static readonly userOrderTableColumn =
   ['order', 'creationDate', 'product', 'title', 'quantity', 'amount', 'orderStatus'];
  public static readonly printingEditionTableColumns = ['id', 'name', 'description', 'category', 'author', 'price', 'features' ];
  public static readonly userTableColumns = ['name', 'email', 'userStatus', 'userFeatures'];
  public static readonly cartTableColumns = ['title', 'price', 'quantity', 'amount'];
  public static readonly withCredentials = true;
  public static readonly defaultRememberMe = false;

}
