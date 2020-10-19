﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Helpers;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels 
{
    public class SalesViewModel : Screen
    {
        private IProductEndpoint _productEndpoint;
        private IConfigHelper _configHelper;
        private ProductModel _selectedProduct;
        private ISaleEndPoint _saleEndPoint;
        private BindingList<ProductModel> _products;
        private int _itemQuantity = 1;

        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper, ISaleEndPoint saleEndPoint)
        {
            _productEndpoint = productEndpoint;
            _saleEndPoint = saleEndPoint;
            _configHelper = configHelper;
            
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }
        public async Task LoadProducts()
        {
             
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }

        

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
            }
        }


        

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set 
            { 
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
                
        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set 
            { 
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            { 
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal
        {
            get 
            {
                decimal subTotal = CalculateSubTotal();
                
                return subTotal.ToString("C");
            }
            
        }

        private decimal CalculateSubTotal()
        {
            decimal subTotal = 0;

            foreach (var item in Cart)
            {
                subTotal += item.Product.RetailPrice * item.QuantityInCart;
            }
            return subTotal;
        }

        public string Total
        {
            get
            {
                decimal total = CalculateSubTotal() - CalculateTax();
                return total.ToString("C");
            }

        }

        private decimal CalculateTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate()/100;

            Cart.Where(x => x.Product.IsTaxable).Sum(x => x.Product.RetailPrice * x.Product.QuantityInStock * taxRate);

            //foreach (var item in Cart)
            //{
            //    taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
            //}

            return taxAmount;
        }

        public string Tax
        {
            get
            {
                
                return CalculateTax().ToString("C");
            }

        }

        public bool CanAddToCart
        {
            get
            {
                bool ret = false;

                if (ItemQuantity>0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    ret = true;
                }

                return ret;
            }

        }
        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(XamlGeneratedNamespace => XamlGeneratedNamespace.Product == SelectedProduct);

            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;

                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(item);
            }
            
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool ret = false;

                return ret;
            }

        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanCheckOut
        {
            get
            {
                bool ret = false;

                if (Cart.Count > 0)
                {
                    ret = true;
                }

                return ret;
            }

        }
        public async Task CheckOut()
        {
            SaleModel sale = new SaleModel();

            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProductId = item.Product.Id ,
                    Quantity = item.QuantityInCart
                });
                
            }

            await _saleEndPoint.PostSale(sale);
        }

    }
}
