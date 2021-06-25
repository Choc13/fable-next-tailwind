module Next

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

type AppProps<'P> = 'P

[<AbstractClass; ImportDefault("next/app")>]
type App<'P>(initialProps) =
    inherit PureStatelessComponent<AppProps<'P>>(initialProps)

    static member getInitialProps(ctx: obj) : JS.Promise<obj> = jsNative

type DocumentProps<'P> = 'P

[<AbstractClass; ImportDefault("next/document")>]
type Document<'P>(initialProps) =
    inherit PureStatelessComponent<DocumentProps<'P>>(initialProps)

    static member getInitialProps(ctx: obj) : JS.Promise<obj> = jsNative

module Document =

    type OriginProps =
        | CrossOrigin of string
        | Nonce of string

    let html (props: IHTMLProp seq) children : ReactElement =
        let propsObject = keyValueList CaseRules.LowerFirst props
        ofImport "Html" "next/document" propsObject children

    let head (props: OriginProps seq) children : ReactElement =
        let propsObject = keyValueList CaseRules.LowerFirst props
        ofImport "Head" "next/document" propsObject children

    let main (props: IHTMLProp seq) children : ReactElement =
        let propsObject = keyValueList CaseRules.LowerFirst props
        ofImport "Main" "next/document" propsObject children

    let nextScript (props: OriginProps seq) children : ReactElement =
        let propsObject = keyValueList CaseRules.LowerFirst props
        ofImport "NextScript" "next/document" propsObject children
