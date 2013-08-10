using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SplashEstendida
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        public SplashPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Frame que será criado no construtor da SplashPage para ser utilizado posteriormente na navegação à página inicial do aplicativo.
        /// </summary>
        Frame rootFrame;

        /// <summary>
        /// Construtor que adicionalmente recebe a SplashScreen da aplicação.
        /// </summary>
        /// <param name="splash">A SplashScreen da aplicação.</param>
        public SplashPage(Windows.ApplicationModel.Activation.SplashScreen splash) : this()
        {
            Grid a;
            //a.Background = new SolidColorBrush(Windows.UI.Colors.gray

            Window.Current.SizeChanged += SplashPage_SizeChanged;

            _splash = splash;
            if (_splash != null)
            {
                _splash.Dismissed += DismissedEventHandler;
                PosicionarImagem();
            }

            // Criando um Frame no construtor para ser utilizado na navegação para a página principal do aplicativo.
            rootFrame = new Frame();
        }

        /// <summary>
        /// Handler assíncrono que fará o trabalho de carregamento dos recursos da aplicação e navegará para a página principal quando tiver concluído.
        /// </summary>
        /// <param name="sender">A SplashScreen da aplicação.</param>
        /// <param name="e">Informações sobre o evento Dismissed da SplashScreen.</param>
        async void DismissedEventHandler(Windows.ApplicationModel.Activation.SplashScreen sender, object e)
        {
            // Neste ponto você deve fazer o carregamento de recursos da sua aplicação. 
            // Como se trata de um exemplo, estamos somente fazendo a thread esperar 3 segundos antes de prosseguir.
            new System.Threading.ManualResetEvent(false).WaitOne(3000);

            // Quando o carregamento estiver concluído, navegamos para a página principal do aplicativo.
            await rootFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                rootFrame.Navigate(typeof(MainPage), true);
                Window.Current.Content = rootFrame;
            });
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Atributo que armazena uma referência à SplashScreen da aplicacao. Esse atributo é inicializado no construtor da SplashPage.
        /// </summary>
        private Windows.ApplicationModel.Activation.SplashScreen _splash;

        /// <summary>
        /// Handler para o evento SizeChanged da SplashPage. Aqui nós chamamos o método PosicionarImagem, que vai posicionar apropriadamente a SplashImage
        /// de acordo com o estado visual corrente (snapped, full, etc).
        /// </summary>
        /// <param name="sender">O objeto que invocou o evento SizeChanged.</param>
        /// <param name="e">Informacoes sobre o evento SizeChanged.</param>
        void SplashPage_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            PosicionarImagem();
        }

        /// <summary>
        /// Posiciona a SplashImage apropriadamente de acordo com o estado visual corrente (snapped, full, etc).
        /// </summary>
        void PosicionarImagem()
        {
            if (_splash != null)
            {
                Rect splashImageRect = _splash.ImageLocation;

                SplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
                SplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
                SplashImage.Height = splashImageRect.Height;
                SplashImage.Width = splashImageRect.Width;
            }
        }
    }
}
