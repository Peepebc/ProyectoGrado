import { useRouter } from 'next/router'
import Context from '../Context.js'
import { Modal, Rating, StyledRating } from '@mui/material';
import { AiOutlineHeart, AiFillHeart, AiOutlineEye, AiOutlineEyeInvisible,AiOutlineStar } from 'react-icons/ai';
import { useContext, useState } from 'react';
import ResenaPelicula from '../components/ResenaPelicula';
export default function Pelicula({params}){

    const router = useRouter()
    const {user, setUser} = useContext(Context)
    const {id} = router.query
    const [isFav, setIsFav] = useState(false)
    const [isWatched, setIsWatched] = useState(false)
    const [isOpen,setOpen] = useState(false)
    const [rating,setRating] = useState(0)

    const openModal = () =>{
        setOpen(!isOpen)
        setRating(0)
    }
    return (
        <>
            <section>
                <div className='flex flex-col md:flex-row gap-10 items-center md:justify-center py-10'>
                    <img className='w-72 rounded-xl' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/1580999639_454991_1581001437_album_normal.jpg" alt="" />
                    <div className='flex flex-col w-96 gap-5'>
                        <div className='flex flex-row gap-3'>
                            <p className='font-bold'>JOJO RABIT</p>
                            <p>2019</p>
                            <p>Director: Taikia daadsd</p>
                        </div>
                        <p className='text-lg'>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
                        Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo conseq Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo conseq
                        </p>
                        <p>
                            GENRES: Belic, Drama
                        </p>
                    </div>
                    <div className='flex flex-col'>
                        <p className='p-2 border-b-2'>RATINGS</p>
                        <div className='flex flex-col text-center items-center gap-5 py-5'>
                            <p className='text-4xl'>4.3/5</p>
                            <Rating className='!text-green-700' name="half-rating-read" size='large' defaultValue={4.3} precision={0.5} readOnly />
                            <div className='flex flex-row gap-5 items-center'>
                                <button onClick={openModal} className="w-36 rounded-lg p-2 bg-gray-100 text-black">
                                    REVIEW
                                </button>
                                {!isFav ? <AiOutlineHeart onClick={() => setIsFav(!isFav)} className='text-4xl'/> : <AiFillHeart onClick={() => setIsFav(!isFav)} className='text-4xl text-green-700'/>}
                                {!isWatched ? <AiOutlineEye onClick={() => setIsWatched(!isWatched)} className='text-4xl'/> : <AiOutlineEyeInvisible onClick={() => setIsWatched(!isWatched)} className='text-4xl text-green-700'/>}
                                
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section className='flex flex-col p-10 items-center'>
                <div className='md:w-2/3'>
                    <div className='flex justify-between border-b-2 px-3 mb-5'>
                        <p>ULTIMAS RESEÑAS</p>
                    </div>
                    <div className='flex flex-col gap-5'>
                        <ResenaPelicula/>
                        <ResenaPelicula/>
                        <ResenaPelicula/>
                        <ResenaPelicula/>
                    </div>
                </div>
            </section>


            <Modal
            open={isOpen}
            onClose={openModal}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
            >
                <div className='flex flex-col items-center md:flex-row bg-blue-950 rounded-xl gap-5 p-5 absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2'>
                    <img className='w-56 rounded-xl' src="https://moviebox-pelis.s3.us-east-005.backblazeb2.com/Peliculas/1580999639_454991_1581001437_album_normal.jpg" alt="" />
                    <div className='flex flex-col gap-5'>
                        <h1 className='font-extrabold text-xl'>JOJO RABIT</h1>
                        <textarea className='p-3 text-black rounded-xl resize-none focus:outline-none' name="resena" rows={7} id="" placeholder='Escriba su reseña' ></textarea>
                        <div className='flex items-center justify-between gap-5'>
                            
                            <Rating 
                            emptyIcon={<AiOutlineStar className='text-green-700'/>}
                            className='!text-green-700' 
                            name="half-rating-read" 
                            size='large' 
                            onChange={(_,value) => {setRating(value)}} 
                            defaultValue={rating} 
                            precision={0.5} />
                            <button className="w-36 rounded-lg p-2 bg-gray-100 text-black">
                                REVIEW
                            </button>
                        </div>
                    </div>
                </div>
            </Modal>
        </>
    )
}