using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;
using Services;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductService iProductService;
        private readonly ICategoryService iCategoryService;
        public MainWindow()
        {
            InitializeComponent();
            iProductService = new ProductService();
            iCategoryService = new CategoryService();
        }

        public void LoadCategoryList()
        {
            try
            {
                var catList = iCategoryService.GetCategories();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading categories");
            }
        }

        public void LoadProductList()
        {
            try
            {
                var productList = iProductService.GetProducts().ToList();
                dgData.ItemsSource = null;
                dgData.ItemsSource = productList;
                dgData.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
            finally
            {
                //LoadProductList();
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ProductName = txtProductName.Text;
                product.UnitPrice = Decimal.Parse(txtPrice.Text);
                product.UnitInStock = short.Parse(txtUnitsInStock.Text);
                product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                iProductService.SaveProduct(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }


        //private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    DataGrid dataGrid = sender as DataGrid;
        //    DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
        //    DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
        //    string id = ((TextBlock)RowColumn.Content).Text;
        //    Product product = iProductService.GetProductById(Int32.Parse(id));
        //    txtProductID.Text = product.ProductId.ToString();
        //    txtProductName.Text = product.ProductName;
        //    txtPrice.Text = product.UnitPrice.ToString();
        //    txtUnitsInStock.Text = product.UnitInStock.ToString();
        //    cboCategory.SelectedValue = product.CategoryId;
        //}
        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null) return;

            var selectedProduct = dataGrid.SelectedItem as Product;
            if (selectedProduct != null)
            {
                txtProductID.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName;
                txtPrice.Text = selectedProduct.UnitPrice.ToString();
                txtUnitsInStock.Text = selectedProduct.UnitInStock.ToString();
                cboCategory.SelectedValue = selectedProduct.CategoryId;
            }
            else
            {
                resetInput();
            }
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Product product = new Product();
                    product.ProductId = Int32.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = decimal.Parse(txtPrice.Text);
                    product.UnitInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                    iProductService.UpdateProduct(product);
                }
                else
                {
                    MessageBox.Show("Please select a product to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtProductID.Text.Length > 0)
                {
                    Product product = new Product();
                    product.ProductId = Int32.Parse(txtProductID.Text);
                    product.ProductName = txtProductName.Text;
                    product.UnitPrice = decimal.Parse(txtPrice.Text);
                    product.UnitInStock = short.Parse(txtUnitsInStock.Text);
                    product.CategoryId = Int32.Parse(cboCategory.SelectedValue.ToString());
                    iProductService.DeleteProduct(product);
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void resetInput()
        {
            try
            {
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtPrice.Text = "";
                txtUnitsInStock.Text = "";
                cboCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting input fields: " + ex.Message);
            }
        }

    }
}