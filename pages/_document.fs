module Pages.Document

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Node.Api

[<Literal>]
let Title = "Symbolica"

[<Literal>]
let Description =
    "Run your code forall inputs. Find bugs faster with Symbolica."

[<Literal>]
let Image = "/full-logo-dark-1200x628.png"

[<Literal>]
let Url = "https://symbolica.dev"

let csp nonce isDev =
    let nonceDirective = $"'nonce-{nonce}'"

    $"""
default-src;
script-src {nonceDirective} 'strict-dynamic' 'unsafe-eval' 'unsafe-inline';
style-src {if isDev then "*" else nonceDirective} 'unsafe-inline';
img-src 'self';
font-src https://fonts.gstatic.com;
frame-src https://app.netlify.com;
{if isDev then
     "connect-src 'self';"
 else
     ""}
base-uri 'none';
form-action 'none';
frame-ancestors 'none';
{if isDev then
     ""
 else
     "require-trusted-types-for 'script'"};
upgrade-insecure-requests;
block-all-mixed-content;
report-uri https://symbolica.report-uri.com/r/d/csp/enforce"""
        .Replace(System.Environment.NewLine, "")

type DocumentProps =
    abstract Nonce : string
    inherit Next.DocumentProps

[<AttachMembers>]
type Document(initialProps) =
    inherit Next.Document<DocumentProps>(initialProps)

    static member getInitialProps(ctx) =
        promise {
            let! initialProps = Next.Document<_>.getInitialProps (ctx)
            let nonce = "anonce" // TODO: Generate a proper nonce

            let csp =
                csp nonce (``process``.env?NODE_ENV = "development")

            match ctx.res with
            | Some res -> res.setHeader ("Content-Security-Policy", !!csp)
            | None -> ()

            return JS.Constructors.Object.assign (createEmpty<DocumentProps>, initialProps, {| Nonce = nonce |})
        }

    override this.render() =
        let nonce = this.props.Nonce

        Next.Document.html [ Lang "en" ] [
            Next.Document.head [ Next.Document.Nonce nonce ] [
                meta [ Name "title"
                       HTMLAttr.Content Title ]
                meta [ Name "description"
                       HTMLAttr.Content Description ]
                meta [ Name "og:type"
                       HTMLAttr.Content "website" ]
                meta [ Name "og:url"
                       HTMLAttr.Content Url ]
                meta [ Name "og:title"
                       HTMLAttr.Content Title ]
                meta [ Name "og:description"
                       HTMLAttr.Content Description ]
                meta [ Name "og:image"
                       HTMLAttr.Content Image ]
                meta [ Name "twitter:card"
                       HTMLAttr.Content Image ]
                meta [ Name "twitter:image"
                       HTMLAttr.Content Image ]
                meta [ Name "twitter:url"
                       HTMLAttr.Content Url ]
                meta [ Name "twitter:title"
                       HTMLAttr.Content Title ]
                meta [ Name "twitter:description"
                       HTMLAttr.Content Description ]
                link [ Rel "shortcut icon"
                       Type "image/png"
                       Href "/favicon-16x16.png"
                       Sizes "16x16"
                       HTMLAttr.Custom("Nonce", nonce) ]
                link [ Rel "shortcut icon"
                       Type "image/png"
                       Href "/favicon-32x32.png"
                       Sizes "32x32"
                       HTMLAttr.Custom("Nonce", nonce) ]
                link [ Rel "shortcut icon"
                       Type "image/png"
                       Href "/favicon-96x96.png"
                       Sizes "96x96"
                       HTMLAttr.Custom("Nonce", nonce) ]
                link [ Rel "apple-touch-icon-precomposed"
                       Type "image/png"
                       Href "/favicon-152x152.png"
                       Sizes "152x152"
                       HTMLAttr.Custom("Nonce", nonce) ]
                link [ Rel "apple-touch-icon-precomposed"
                       Type "image/png"
                       Href "/favicon-167x167.png"
                       Sizes "167x167"
                       HTMLAttr.Custom("Nonce", nonce) ]
                link [ Rel "apple-touch-icon-precomposed"
                       Type "image/png"
                       Href "/favicon-180x180.png"
                       Sizes "180x180"
                       HTMLAttr.Custom("Nonce", nonce) ]
                link [ Rel "stylesheet"
                       Href "https://fonts.googleapis.com/css2?family=Roboto&family=JetBrains%20Mono&family=JetBrains"
                       HTMLAttr.Custom("nonce", nonce) ]
            ]
            body [ ClassName "font-sans font-base font-normal leading-relaxed text-primary" ] [
                Next.Document.main [] []
                Next.Document.nextScript [ Next.Document.Nonce nonce ] []
            ]
        ]

exportDefault jsConstructor<Document>
