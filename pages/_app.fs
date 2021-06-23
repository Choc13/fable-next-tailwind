module Pages.App

open Fable.Core.JsInterop
open Fable.React
open NextJS

importAll "../styles/global.css"

type AppProps =
    { Component: ReactElementType<obj>
      PageProps: obj }

let app =
    FunctionComponent.Of(
        (fun { Component = pageComponent
               PageProps = pageProps } ->
            fragment [] [
                Next.Head(
                    Head.children [ title [] [ str "Symbolica" ]
                                    meta [ Props.Name "viewport"
                                           Props.Content "width=device-width, initial-scale=1.0" ] ]
                )
                Nav.View()
                ReactElementType.create pageComponent pageProps []
                Footer.view
            ]),
        memoizeWith = equalsButFunctions
    )

app |> exportDefault
