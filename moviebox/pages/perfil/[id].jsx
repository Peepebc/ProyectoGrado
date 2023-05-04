import Link from "next/link";
import { useRouter } from 'next/router'
import { parseCookies } from "../../helpers"
import { useContext, useEffect } from "react";
import { useCookies } from "react-cookie";
import Context from '../Context.js'
import UltimasListas from "../components/UltimasListas";
import UltimasPeliculas from "../components/UltimasPeliculas";
import UltimasResenas from "../components/UltimasResenas";
import Favoritas from "../components/Favoritas";
import ResenaPelicula from "../components/ResenaPerfil";

Profile.getInitialProps = async ({ req, res }) => {
    const data = parseCookies(req)
  
  if (res) {
      if (Object.keys(data).length === 0 && data.constructor === Object) {
        res.writeHead(301, { Location: "/login" })
        res.end()
      }
    }
    return {
      data: data && data,
    }
  }

export default function Profile({data}){

    const [cookies, setCookie, removeCookie] = useCookies([])
    const router = useRouter()
    const {id} = router.query
    const {user, setUser} = useContext(Context)
    console.log({user})

    useEffect(() => {
        fetch("/api/Usuarios/Validame",{
            method: 'GET',
            headers: { 'Content-type': 'application/json', 'Authorization': 'Bearer ' + data.jwt },
            credentials: 'same-origin'
        }).then(res => res.json()).then(data => {
            setUser(data)
        })
    }, []);

    const logOut = () =>{
      setUser(null)
      removeCookie("jwt",{path:'/'})
      router.push('/');
    }

    return (
      <>
        <section className="flex flex-row p-10 pl-24 items-center justify-between px-14">
          <div className="flex items-center gap-5">
            <img className='rounded-full w-32 h-32 shadow-black shadow-lg' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Pfps/pepe2.jpg" alt="" />
            <p className='md:text-4xl font-bold'>Pepebc</p>
          </div>
          {user && user.id==id ?
          <div className="flex gap-5 ">
            <button onClick={logOut} type="submit" className="w-36 rounded-lg p-2 bg-gray-100 text-black">
                LOGOUT
            </button>
            <button type="submit" className="w-36 rounded-lg p-2 bg-gray-100 text-black">
                EDITAR PERFIL
            </button>
          </div>
          :null}
        </section>
        <UltimasPeliculas/>
        <Favoritas/>
        <section className='p-10'>
          <div className='flex justify-between border-b-2 px-3 mb-5'>
            <p>ULTIMAS RESEÑAS</p>
            <Link href={'/resenas'}> VER TODAS</Link>
          </div>
          <div className='flex flex-wrap gap-5 justify-evenly'>
            <ResenaPelicula/>
            <ResenaPelicula/>
            <ResenaPelicula/>
            <ResenaPelicula/>
          </div>
        </section>
        <UltimasListas/>
      </>
    )
}
