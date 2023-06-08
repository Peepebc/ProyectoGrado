/** @type {import('next').NextConfig} */
module.exports = {
  skipTrailingSlashRedirect: true,
  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: 'http://localhost:5086/:path*' // Proxy to Backend
      }
    ]
  }
}