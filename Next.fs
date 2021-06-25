module Next

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Node.Http

// fsharplint:disable InterfaceNames MemberNames

module Node =
    module Url =
        type UrlObject =
            abstract auth : string option
            abstract hash : string option
            abstract host : string option
            abstract hostname : string option
            abstract href : string option
            abstract pathname : string option
            abstract protocol : string option
            abstract search : string option
            abstract slashes : bool option
            abstract port : U2<string, int> option
            abstract query : U2<string, obj> option

module Mitt =
    type Handler = obj array -> unit

    type MittEmitter<'T> =
        abstract on : 'T * Handler -> unit
        abstract off : 'T * Handler -> unit
        abstract emit : 'T * obj array -> unit

type Url = U2<string, Node.Url.UrlObject>

type DomainLocale =
    abstract http : bool option with get, set
    abstract domain : string with get, set
    abstract locales : string array with get, set
    abstract defaultLocale : string with get, set

type DomainLocales = DomainLocale array

type StyleSheetTuple =
    abstract href : string with get, set
    abstract text : string with get, set

type GoodPageCache =
    abstract page : ReactElement with get, set
    abstract ``mod`` : obj with get, set
    abstract styleSheets : StyleSheetTuple array with get, set

[<AbstractClass>]
type ErrorWithStatusCode() =
    inherit System.Exception()
    abstract statusCode : int option with get, set

type BaseContext =
    abstract res : ServerResponse option with get, set

[<AbstractClass>]
type NextComponentType<'C, 'IP, 'P>(initialProps) =
    inherit PureStatelessComponent<'P>(initialProps)
    abstract getInitialProps : 'C -> JS.Promise<'IP>

type AppInitialProps =
    abstract pageProps : obj with get, set

type AppTreeType = PureStatelessComponent<AppInitialProps>

type NextPageContext =
    abstract err : ErrorWithStatusCode option with get, set
    abstract req : IncomingMessage option with get, set
    abstract pathname : string with get, set
    abstract query : obj with get, set
    abstract asPath : string option with get, set
    abstract locale : string option with get, set
    abstract locales : string array option with get, set
    abstract defaultLocale : string option with get, set
    abstract AppTree : AppTreeType with get, set
    inherit BaseContext

module rec Router =

    type ResetScroll = {| x: float; y: float |}

    [<StringEnum>]
    type RouterEvent =
        | RouteChangeStart
        | BeforeHistoryChange
        | RouteChangeComplete
        | RouteChangeError
        | HashChangeStart
        | HashChangeComplete

    type RouteProperties =
        abstract shallow : bool with get, set

    type TransitionOptions =
        abstract shallow : bool option with get, set
        abstract locale : string option with get, set
        abstract scroll : bool option with get, set

    type NextHistoryState =
        abstract url : string with get, set
        abstract ``as`` : string with get, set
        abstract options : TransitionOptions with get, set

    type PrefetchOptions =
        abstract priority : bool option with get, set
        abstract locale : U2<string, bool> option with get, set

    type InitialPrivateRouteInfo =
        abstract Component : ReactElement with get, set
        abstract __N_SSG : bool option with get, set
        abstract __N_SSP : bool option with get, set
        abstract props : obj option with get, set
        abstract err : System.Exception option with get, set
        abstract errror : obj option with get, set

    type CompletePrivateRouteInfo =
        abstract styleSheets : StyleSheetTuple array with get, set
        inherit InitialPrivateRouteInfo

    type PrivateRouteInfo = U2<InitialPrivateRouteInfo, CompletePrivateRouteInfo>

    type AppProps =
        abstract Component : ReactElement with get, set
        abstract err : System.Exception with get, set
        abstract router : Router with get, set

    type AppComponent = PureStatelessComponent<AppProps>

    type Subscription = PrivateRouteInfo * AppComponent * ResetScroll option -> JS.Promise<unit>

    type BeforePopStateCallback = NextHistoryState -> bool

    type ComponentLoadCancel = (unit -> unit) option

    [<StringEnum>]
    type HistoryMethod =
        | ReplaceState
        | PushState

    [<AbstractClass>]
    type RouteInfoError() =
        inherit System.Exception()
        abstract code : obj with get, set
        abstract cancelled : bool with get, set

    type BaseRouter =
        abstract route : string with get, set
        abstract pathname : string with get, set
        abstract query : obj with get, set
        abstract asPath : string with get, set
        abstract basePath : string with get, set
        abstract locale : string option with get, set
        abstract locales : string array option with get, set
        abstract defaultLocale : string with get, set
        abstract domainLocales : DomainLocales with get, set
        abstract isLocaleDomain : bool with get, set

    type NextRouter =
        abstract push : Url * Url option * TransitionOptions -> JS.Promise<bool>
        abstract replace : Url * Url option * TransitionOptions -> JS.Promise<bool>
        abstract reload : unit -> unit
        abstract back : unit -> unit
        abstract prefetch : string * string * PrefetchOptions -> JS.Promise<unit>
        abstract beforePopState : BeforePopStateCallback -> unit
        abstract events : Mitt.MittEmitter<RouterEvent> with get, set
        abstract isFallback : bool with get, set
        abstract isReady : bool with get, set
        abstract isPreview : bool with get, set
        inherit BaseRouter

    type Router =
        abstract components : obj with get, set
        abstract sdc : obj with get, set
        abstract sdr : obj with get, set
        abstract sub : Router.Subscription with get, set
        abstract clc : ComponentLoadCancel with get, set
        abstract pageLoader : obj with get, set
        abstract _bps : BeforePopStateCallback option with get, set
        abstract events : Mitt.MittEmitter<RouterEvent> with get, set
        abstract _wrapApp : AppComponent -> obj
        abstract isSsr : bool with get, set
        abstract isFallback : bool with get, set
        abstract _inFlightRoute : string option with get, set
        abstract _shallow : bool option with get, set
        abstract isReady : bool with get, set
        abstract isPreview : bool with get, set
        abstract onPopState : PopStateEvent -> unit
        abstract reload : unit -> unit
        abstract back : unit -> unit
        abstract push : Url * Url option * TransitionOptions -> JS.Promise<bool>
        abstract replace : Url * Url option * TransitionOptions -> JS.Promise<bool>
        abstract changeState : HistoryMethod * string * string * TransitionOptions option -> unit

        abstract handleRouteInfoError :
            RouteInfoError * string * obj * string * RouteProperties * bool option ->
            JS.Promise<CompletePrivateRouteInfo>

        abstract getRouteInfo :
            string * string * obj * string * string * RouteProperties -> JS.Promise<PrivateRouteInfo>

        abstract set : string * string * obj * string * PrivateRouteInfo * Router.ResetScroll -> JS.Promise<unit>
        abstract beforePopState : BeforePopStateCallback -> unit
        abstract onlyAHashChange : string -> bool
        abstract scrollToHash : string -> unit
        abstract urlIsNew : string -> bool
        abstract prefetch : string * string * PrefetchOptions -> JS.Promise<unit>
        abstract fetchComponent : string -> JS.Promise<GoodPageCache>
        abstract getInitialProps : ReactElement * NextPageContext -> JS.Promise<obj>
        abstract abortComponentLoad : string * RouteProperties -> unit
        abstract notify : PrivateRouteInfo * Router.ResetScroll option -> JS.Promise<unit>
        inherit NextRouter

type BuildManifest =
    abstract devFiles : string array
    abstract ampDevFiles : string array
    abstract polyfillFiles : string []
    abstract lowPriorityFiles : string []
    abstract pages : obj
    abstract ampFirstPages : string array

type DefaultNextComponentType = NextComponentType<NextPageContext, obj, obj>

type AppContextType<'R when 'R :> Router.NextRouter> =
    abstract Component : NextComponentType<NextPageContext, obj, obj> with get, set
    abstract AppTree : AppTreeType with get, set
    abstract ctx : NextPageContext with get, set
    abstract router : 'R with get, set

type AppPropsType<'R, 'P when 'R :> Router.NextRouter> =
    abstract Component : NextComponentType<NextPageContext, obj, 'P>
    abstract router : 'R with get, set
    abstract __N_SSG : bool option with get, set
    abstract __N_SSP : bool option with get, set

type AppType =
    NextComponentType<AppContextType<Router.NextRouter>, AppInitialProps, AppPropsType<Router.NextRouter, obj>>

type Enhancer<'C> = 'C -> 'C

type ComponentsEnhancer =
    U2<{| enhanceApp: Enhancer<AppType> option
          enhanceComponent: Enhancer<DefaultNextComponentType> option |}, Enhancer<DefaultNextComponentType>>

type RenderPageResult =
    abstract html : string with get, set
    abstract head : ReactElement option array option with get, set

type RenderPage = ComponentsEnhancer option -> JS.Promise<RenderPageResult>

type NEXT_DATA =
    abstract props : obj with get, set
    abstract page : string with get, set
    abstract query : obj with get, set
    abstract buildId : string with get, set
    abstract assetPrefix : string option with get, set
    abstract runtimeConfig : obj option with get, set
    abstract nextExport : bool option with get, set
    abstract autoExport : bool option with get, set
    abstract isFallback : bool option with get, set
    abstract dynamicIds : U2<string, int> array option with get, set
    abstract err : ErrorWithStatusCode option with get, set
    abstract gsp : bool option with get, set
    abstract gssp : bool option with get, set
    abstract customServer : bool option with get, set
    abstract gip : bool option with get, set
    abstract appGip : bool option with get, set
    abstract locale : string option with get, set
    abstract locales : string array option with get, set
    abstract defaultLocale : string option with get, set
    abstract domainLocales : DomainLocales option with get, set
    abstract scriptLoader : obj array option with get, set
    abstract isPreview : bool option with get, set

type AppContext = AppContextType<Router.Router>
type AppProps<'P> = AppPropsType<Router.Router, 'P>

[<AbstractClass; ImportDefault("next/app")>]
type App<'P, 'CP, 'S when 'P :> AppProps<'CP>>(initialProps) =
    inherit Component<'P, 'S>(initialProps)
    static member origGetInitialProps(_: AppContext) : JS.Promise<obj> = jsNative
    static member getInitialProps(_: AppContext) : JS.Promise<obj> = jsNative

type DocumentInitialProps =
    abstract styles : U2<ReactElement array, ReactElement> option with get, set
    inherit RenderPageResult

type DocumentProps =
    abstract __NEXT_DATA__ : NEXT_DATA with get, set
    abstract dangerousAsPath : string with get, set

    abstract docComponentsRendered :
        {| Html: bool option
           Main: bool option
           Head: bool option
           NextScript: bool option |} with get, set

    abstract buildManifest : BuildManifest with get, set
    abstract ampPath : string with get, set
    abstract inAmpMode : bool with get, set
    abstract hybridAmp : bool with get, set
    abstract isDevelopment : bool with get, set
    abstract dynamicImports : string array with get, set
    abstract assetPrefix : string option with get, set
    abstract canonicalBase : string with get, set
    abstract headTags : obj array with get, set
    abstract unstable_runtimeJS : bool option with get, set
    abstract unstable_JsPerload : bool option with get, set
    abstract devOnlyCacheBusterQueryString : string with get, set

    abstract scriptLoader :
        {| afterInteractive: string array option
           beforeInteractive: obj array option |}

    abstract locale : string option with get, set
    abstract disableOptimizedLoading : bool option with get, set
    inherit DocumentInitialProps

type DocumentContext =
    abstract renderPage : RenderPage
    inherit NextPageContext

[<ImportDefault("next/document")>]
type Document<'P when 'P :> DocumentProps>(initialProps) =
    inherit PureStatelessComponent<'P>(initialProps)

    static member getInitialProps(_: DocumentContext) : JS.Promise<DocumentInitialProps> = jsNative

    abstract renderDocument : Document<'P> * DocumentProps -> ReactElement
    default _.renderDocument(_: Document<'P>, __: DocumentProps) : ReactElement = jsNative

    override _.render() = jsNative

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
