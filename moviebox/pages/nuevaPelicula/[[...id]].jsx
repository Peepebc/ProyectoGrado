import Link from "next/link";
import Image from 'next/image';
import logo from '../../public/resources/moviebox.png';
import peliculas from '../../public/resources/peliculas.png';
import { useForm } from "react-hook-form";
import { useRouter  } from 'next/router';
import { useContext, useEffect, useState } from "react";
import Context from "../Context";

export default function NuevaPelicula(){

const {user} = useContext(Context)
const { register, handleSubmit} = useForm();
const router = useRouter();
const {id} = router.query
const [urlImagen,setUrlImagen] = useState(null)

async function onSubmit (data) {

    var formData = new FormData()

    formData.append("Titulo",data.Titulo)
    formData.append("Director",data.Director)
    formData.append("Generos",data.Generos)
    formData.append("Fecha",data.Fecha)
    formData.append("Descripcion",data.Descripcion)
    formData.append("Imagen",data.Imagen[0],data.Imagen[0].name)
    formData.append("Valoracion",0)

    const nuevaPeli = await fetch('http://localhost:5086/Peliculas/AnadirPelicula', {
        method: 'POST',
        body:formData,
    }).then(res => res.json()).then(data => {return data})

    if(nuevaPeli) router.push(`/`)
}


    return (
        <>
        <div className="flex justify-center items-center ">
            <div className=" md:flex flex-row shadow-2xl shadow-black rounded-lg">
                <div className="rounded-lg p-10   w-auto h-auto" style={{backgroundColor: "#1F2E55"}}>
                    <h1 className="text-4xl text-center">AÑADIR PELICULA</h1>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="user">Titulo</label>
                            <input {...register('Titulo')} className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="text"/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="user">Director</label>
                            <input {...register('Director')} className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="text"/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="user">Generos</label>
                            <input {...register('Generos')} className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="text"/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="user">Fecha</label>
                            <input {...register('Fecha')} className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="date"/>
                        </div>
                        <div className="flex flex-col py-3">
                            <label  htmlFor="pass">Descripcion</label>
                            <textarea rows={10} cols={40} {...register('Descripcion')} className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2" ></textarea>
                        </div>
                        <div className="flex flex-row py-3 gap-3 items-center">
                            {urlImagen && <img className="w-20 h-32 object-contain" src={urlImagen}/>}
                            <div className="flex flex-col">
                                <label  htmlFor="pass">Imagen</label>
                                <input rows={10} cols={40} {...register('Imagen')} onChange={(value)=>{setUrlImagen(URL.createObjectURL(value.target.files[0]))}} className="focus:outline-none rounded-lg border border-gray-100 bg-transparent p-2"  type="file"/>
                            </div>
                        </div>
                        <div className="flex justify-center py-4">
                            <button type="submit" className="rounded-lg p-2 bg-gray-100 text-black">
                                AÑADIR PELICULA
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
        </>
    )
}
