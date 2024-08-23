const linkAdmin = [
  {
    link: '/admin/dashboard',
    icon: 'fas fa-chart-bar',
    title: 'DashBoard',
    expend: false,
  },
  {
    link: '/admin/business',
    icon: 'fas fa-building',
    title: 'Business',
    expend: [
      {
        link: '/admin/business/size',
        icon: 'fas fa-compress-arrows-alt',
        title: 'Size',
      },
      {
        link: '/admin/business/coin-package',
        icon: 'fas fa-cube',
        title: 'Coin Package',
      },
      {
        link: '/admin/business/category',
        icon: 'fas fa-th',
        title: 'Category',
      },
      {
        link: '/admin/business/brand',
        icon: 'fas fa-copyright',
        title: 'Brand',
      },
      {
        link: '/admin/business/report-category',
        icon: 'fas fa-file-alt',
        title: 'Report category',
      },
      {
        link: '/admin/business/color',
        icon: 'fas fa-tint',
        title: 'Color',
      },
      {
        link: '/admin/business/subscription-package',
        icon: 'fas fa-gift',
        title: 'Subcription package',
      },
    ],
   
  },
  {
    link: '/admin/users',
    icon: 'fas fa-users',
    title: 'Users',
    expend: [
      {
        link: '/admin/users',
        icon: 'fas fa-user',
        title: 'List'
      },
      {
        link: '/admin/Role',
        icon: 'fas fa-user-tag',
        title: 'Role',
      },
      {
        link: '/admin/assign-role',
        icon: 'fas fa-pencil-alt',
        title: 'Assign role',
      },
     
    ],
  },
  {
    link: '/admin/approve',
    icon: 'fas fa-check',
    title: 'Approve post',
    expend: false,
  },
  {
    link: '/admin/check-report',
    icon: 'fas fa-tools',
    title: 'Check report',
    expend: false,
  },
 
  {
    link: '/admin/statistic',
    icon: 'fas fa-chart-pie',
    title: 'Statistic',
    expend: [
      {
        link: '/admin/statistic/post',
        icon: 'fas fa-newspaper',
        title: 'Post',
      },
      {
        link: '/admin/statistic/report',
        icon: 'fas fa-comment',
        title: 'Report violations',
      },
      
    ],
   
  },
  {
    link: '/admin/logout',
    icon: 'fas fa-sign-out-alt',
    title: 'Logout',
    expend: false,
  },
];

export default linkAdmin;
