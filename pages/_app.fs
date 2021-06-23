module Pages.Index

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open NextJS
open Footer

importAll "${entryDir}/styles/global.css"

let home () =
    div [] [ 
        Next.Head(
           Head.children [
               title [] [ str "Symbolica" ]
               meta [ Name "title"; Props.Content "Symbolica" ] ]
        )
        str "Fable yo Next.js"
        footer()
    ]

home |> exportDefault
