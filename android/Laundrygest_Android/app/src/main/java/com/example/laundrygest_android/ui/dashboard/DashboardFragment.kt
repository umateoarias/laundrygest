package com.example.laundrygest_android.ui.dashboard

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.laundrygest_android.PrincipalActivity
import com.example.laundrygest_android.data.LaundrygestCrudApi
import com.example.laundrygest_android.databinding.FragmentDashboardBinding
import com.example.laundrygest_android.recyclerViewAdapter

private const val ARG_CLIENT_CODE = "_clientCode"

class DashboardFragment : Fragment() {
    private var _clientCode: Int? = null
    private var _binding: FragmentDashboardBinding? = null
    private val dashboardViewModel : DashboardViewModel by activityViewModels()

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentDashboardBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val crudApi = LaundrygestCrudApi()

        dashboardViewModel.clientCode.observe(viewLifecycleOwner) { code ->
            val collectionList = crudApi.getCollectionsClient(code)
            if (collectionList != null) {
                binding.recyclerViewCollections.adapter = recyclerViewAdapter(collectionList)
                binding.recyclerViewCollections.layoutManager = LinearLayoutManager(this.context)
            }
        }


        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }


    companion object {
        fun newInstance(clientCode: Int) = DashboardFragment().apply {
            arguments = Bundle().apply {
                putInt(ARG_CLIENT_CODE, clientCode)
            }
        }
    }
}