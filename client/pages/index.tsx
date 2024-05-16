import Container from '@/components/customs/Container'
import Loader from '@/components/customs/Loader'
import Head from 'next/head'

export default function Home() {
  return (
    <Container>
      <Head>
        <title>Dotnet Nextjs URL Shortener</title>
      </Head>
      <h1 className="text-xl font-bold mt-2 mb-5">Dotnet Nextjs URL Shortener</h1>
    </Container>
  )
}
