import React, { useEffect, useState } from 'react'
import TableSearch from '../../components/TableSearch'
import { instance } from '../../config/axiosConfig'

  const initValues = {
    id: 0,
    name: '',
    description: '',
    imageUrl: '',
  }
  const types = [
    "number",
    "string",
    "string",
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
    "ID","Name","Description","Create date", "Update date"
]
  
  const actions = {
    getALl: '/ReportCategory/getALl',
    put : '/ReportCategory/update/',
    post: '/ReportCategory/add',
    delete: '/ReportCategory/delete/'
  }
function ReportCategory() {
    
    const [data,setData] = useState([])
  const [value,setValue] = useState([])
  const [reload,setReload] = useState(false)
  const [id,name,description] = value
  const  valueAction = {id,name,description}
  useEffect(()=> { 
    
    instance.get(actions.getALl)
    .then( rs => {

      let temp = new Array()

      rs.data.map(({id,name,description,imageUrl,createdDate,updatedDate}) => {
        
        temp.push({
          id,
          name,
          description,
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

export default ReportCategory
