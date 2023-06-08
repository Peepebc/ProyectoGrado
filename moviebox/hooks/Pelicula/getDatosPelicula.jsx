import { useRouter } from "next/router"
import { useEffect, useState } from "react"

export default function getDatosPelicula(idPelicula){
    
    const [pelicula,setPelicula] = useState([])
    const [reviews,setReviews] = useState(0)
    const [isLoading,setLoading] = useState(false)
    const router = useRouter()
    
      useEffect(()=>{
        if(!router.isReady) return
        fetch(`http://localhost:5086/Peliculas/${idPelicula}`, {
          method: 'GET',
          headers: { 'Content-type': 'application/json'}
      },setLoading(true))
      .then(res => res.json())
      .then(data => {
        setPelicula(data.pelicula)
        setReviews(data.reviews)
        setLoading(false)
      })
      },[router.isReady])
    

      return {pelicula,reviews,isLoading}
}
