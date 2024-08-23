import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import CarouselImage from '../../components/CarouselImage'
import FileReview from '../../components/FileReview'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'
const readOnly = [
  true,
  false,
]
  

const types = [
  'string',
  'string',
  
]
const initValues = {
  id : '',
  name : '',
 
}
const lable = [
  "ID","Name"
]

const actions = {
  getALl: '/Roles',
  put : '/Roles/edit',
  post: '/Roles/add',
  delete: '/CoinPackage/delete/'
}
function Role() {
  const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [id,roleName] = value
  const  valueAction = {id,roleName}
  useEffect(()=> { 
    
    instance.get(actions.getALl)
    .then( rs => {

      let temp = new Array()

      rs.data.map(({id,name}) => {
        
        temp.push({
          id,
          name,
       
        })
      
      })
      setData(temp)
    })
    .catch(e => console.log(e))

  },[reload])
  const setReloaded = () => setReload(!reload)
  

return (
 <TableSearch lable={lable}  data={data} value={value} reload={reload} setReloaded={setReloaded} initValues={initValues} setValue={setValue} valueAction={valueAction}  actions = {actions} readOnly= {readOnly} types={types} />
)
}

export default Role
