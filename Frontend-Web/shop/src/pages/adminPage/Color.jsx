import React, { useEffect, useState } from 'react'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'

  const initValues = {
    id: 0,
    colorName: '',
    hexValue: '',
  }
  const types = [
    "number",
    "string",
    "color",
    "date",
    "date"
 ]
 const readOnly = [
    true,
    false,
    false,
    true,
    true
  ]
    
 const lable = [
    "ID","Name","Review","Create date", "Update date"
]
  
  const actions = {
    getALl: '/Color/getALl',
    put : '/Color/update/',
    post: '/Color/add',
    delete: '/Color/delete/'
  }
function Color() {
    
    const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [id,colorName,hexValue] = value
  const  valueAction = {id,colorName,hexValue}
  useEffect(()=> { 
    
    instance.get(actions.getALl)
    .then( rs => {

      let temp = new Array()

      rs.data.map(({id,colorName,hexValue,createdDate,updatedDate}) => {
        
        temp.push({
          id,
          colorName,
          hexValue,         
          createdDate,
          updatedDate
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

export default Color
