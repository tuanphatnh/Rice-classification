import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { instance } from '../config/axiosConfig'


const date = new Date()
function DashBoardCard({actionsUrl}) {

  const [cardPost,setCardPost] = useState(0)
  const [cardUser,setCardUser] = useState(0)
  const [cardReport,setCardReport] = useState(0)
  
  useEffect(()=>{

    instance.get(`${actionsUrl.getPost}`)
    .then (rs => {
      setCardPost(rs.data.length)
    })
    instance.get(`${actionsUrl.getUser}${date.getFullYear()}`)
    .then (rs => {
      rs.data.map(v => {
        if (v.month == (date.getMonth()+1)){
           setCardUser(v.numberOfNewUser)
        }
      })})
      instance.get(`${actionsUrl.getReport}${date.getFullYear()}`)
    .then (rs => {
      rs.data.map(v => {
        if (v.month == (date.getMonth()+1)){
           setCardReport(v.numberOfNewReport)
        }
      })
    })
  },[])
  return (
   <>
     <div class="list-carddashboard">
      <div class="c-dashboardInfo col-lg-3 col-md-6">
        <div class="wrap">
          <h4 class="heading heading5 hind-font medium-font-weight c-dashboardInfo__title">Post<svg
              class="MuiSvgIcon-root-19" focusable="false" viewBox="0 0 24 24" aria-hidden="true" role="presentation">
              <path fill="none" d="M0 0h24v24H0z"></path>
              <path
                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z">
              </path>
            </svg></h4><span class="hind-font caption-12 c-dashboardInfo__count">{cardPost}</span>
        </div>
      </div>
      <div class="c-dashboardInfo col-lg-3 col-md-6">
        <div class="wrap">
          <h4 class="heading heading5 hind-font medium-font-weight c-dashboardInfo__title">Users<svg
              class="MuiSvgIcon-root-19" focusable="false" viewBox="0 0 24 24" aria-hidden="true" role="presentation">
              <path fill="none" d="M0 0h24v24H0z"></path>
              <path
                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z">
              </path>
            </svg></h4><span class="hind-font caption-12 c-dashboardInfo__count">{cardUser}</span><span
            class="hind-font caption-12 c-dashboardInfo__subInfo"></span>
        </div>
      </div>
      <div class="c-dashboardInfo col-lg-3 col-md-6">
        <div class="wrap">
          <h4 class="heading heading5 hind-font medium-font-weight c-dashboardInfo__title">Report<svg
              class="MuiSvgIcon-root-19" focusable="false" viewBox="0 0 24 24" aria-hidden="true" role="presentation">
              <path fill="none" d="M0 0h24v24H0z"></path>
              <path
                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z">
              </path>
            </svg></h4><span class="hind-font caption-12 c-dashboardInfo__count">{cardReport}</span>
        </div>
      </div>
      <div class="c-dashboardInfo col-lg-3 col-md-6">
        <div class="wrap">
          <h4 class="heading heading5 hind-font medium-font-weight c-dashboardInfo__title">Report ratio <svg
              class="MuiSvgIcon-root-19" focusable="false" viewBox="0 0 24 24" aria-hidden="true" role="presentation">
              <path fill="none" d="M0 0h24v24H0z"></path>
              <path
                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-6h2v6zm0-8h-2V7h2v2z">
              </path>
            </svg></h4><span class="hind-font caption-12 c-dashboardInfo__count">{Intl.NumberFormat('vi-VN',{style:'unit',unit:'percent',maximumFractionDigits: 3}).format((cardReport/cardPost)*100)}</span>
        </div>
      </div>
    </div>
   </>
  )
}

export default DashBoardCard
