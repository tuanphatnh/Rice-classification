import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { instance } from '../config/axiosConfig'

function CarouselImage(props) {
  const {idInfo,report} = props

const [data,setData] = useState([])

useEffect(()=>{
  report !== true ?
    instance.get(`/Product/AdminGetById/${idInfo}`)
    .then( rs => setData(rs.data.product.images))
    :
    instance.get(`/Report/getById/${idInfo}`)
    .then( rs => setData(rs.data.evidences))
},[])

  return (
      <div class="slider">
        <div>
          {
            data.map((_,i)=>
              <a  href={'#slide-'+Number(i+1)}>{i+1}</a>
            )
          }

       
        </div>


  <div class="slides">
    {
      data.map((v,i) => 
        <div key={v.id} id={"slide-"+ Number(i+1)}>
        <img src={report !== true ? v.url : v.imageUrl} alt=''/>
    </div>
        )
    }
    
    
  </div>
</div>
  )
}

export default CarouselImage
