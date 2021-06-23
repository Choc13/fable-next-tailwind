module Pages.Document

open Browser
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

[<Literal>]
let Title = "Symbolica"

[<Literal>]
let Description =
    "Run your code forall inputs. Find bugs faster with Symbolica."

[<Literal>]
let Image = "/full-logo-dark-1200x628.png"

[<Literal>]
let Url = "https://symbolica.dev"

[<AttachMembers>]
type Document(initialProps) =
    inherit Next.Document<obj>(initialProps)

    // [<Emit("getInitialProps($1)")>]
    static member getInitialProps(ctx: obj) =
        promise {
            let! initialProps = Next.Document<_>.getInitialProps (ctx)
            let nonce = "anonce"

            let csp =
                $"default-src 'none'; script-src 'nonce-{nonce}' 'unsafe-inline';"

            match ctx?res with
            | Some res -> 
                console.log("Matched response")
                res?setHeader ("Content-Security-Policy", csp)
            | None -> ()

            console.log(ctx?res)

            let nextProps = initialProps
            nextProps?Nonce <- nonce
            // console.log(nextProps)
            return nextProps
        }

    override this.render() =
        Next.Document.html [ Lang "en" ] [
            Next.Document.head [ Next.Document.Nonce this.props?Nonce ] [
                meta [ Name "title"
                       Props.Content Title ]
                meta [ Name "description"
                       Props.Content Description ]
                meta [ Name "og:type"
                       Props.Content "website" ]
                meta [ Name "og:url"
                       Props.Content Url ]
                meta [ Name "og:title"
                       Props.Content Title ]
                meta [ Name "og:description"
                       Props.Content Description ]
                meta [ Name "og:image"
                       Props.Content Image ]
                meta [ Name "twitter:card"
                       Props.Content Image ]
                meta [ Name "twitter:image"
                       Props.Content Image ]
                meta [ Name "twitter:url"
                       Props.Content Url ]
                meta [ Name "twitter:title"
                       Props.Content Title ]
                meta [ Name "twitter:description"
                       Props.Content Description ]
                link [ Rel "shortcut icon"
                       Type "image/png"
                       Href "/favicon-16x16.png"
                       Sizes "16x16" ]
                link [ Rel "shortcut icon"
                       Type "image/png"
                       Href "/favicon-32x32.png"
                       Sizes "32x32" ]
                link [ Rel "shortcut icon"
                       Type "image/png"
                       Href "/favicon-96x96.png"
                       Sizes "96x96" ]
                link [ Rel "apple-touch-icon-precomposed"
                       Type "image/png"
                       Href "/favicon-152x152.png"
                       Sizes "152x152" ]
                link [ Rel "apple-touch-icon-precomposed"
                       Type "image/png"
                       Href "/favicon-167x167.png"
                       Sizes "167x167" ]
                link [ Rel "apple-touch-icon-precomposed"
                       Type "image/png"
                       Href "/favicon-180x180.png"
                       Sizes "180x180" ]
                link [ Rel "stylesheet"
                       Href "https://fonts.googleapis.com/css2?family=Roboto&family=JetBrains%20Mono&family=JetBrains" ]
            ]
            body [ ClassName "font-sans font-base font-normal leading-relaxed text-primary" ] [
                Next.Document.main [] []
                Next.Document.nextScript [ ] []
            ]
        ]

exportDefault jsConstructor<Document>
