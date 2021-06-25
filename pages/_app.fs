module Pages.App

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open NextJS

importAll "../styles/global.css"

type AppProps =
    { Component: ReactElementType<obj>
      PageProps: obj }

[<AttachMembers>]
type App(initialProps) =
    inherit PureStatelessComponent<obj>(initialProps)

    // Required to force server side rendering which is necessary for generating a nonce on each request
    static member getInitialProps(ctx) = Next.App<_, _, _>.getInitialProps (ctx)

    override this.render() =
        fragment [] [
            Next.Head(
                Head.children [ title [] [ str "Symbolica" ]
                                meta [ Props.Name "viewport"
                                       Props.Content "width=device-width, initial-scale=1.0" ] ]
            )
            Nav.View()
            ReactElementType.create this.props?Component this.props?pageProps []
            Footer.view
        ]

exportDefault jsConstructor<App>
